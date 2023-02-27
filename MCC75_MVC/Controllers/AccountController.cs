using MCC75_MVC.Repositories;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MCC75_MVC.Controllers;

public class AccountController : Controller
{
    private readonly AccountRepository accountRepository;
    private readonly IConfiguration configuration;
    private readonly EmployeeRepository employeeRepository;

    public AccountController(AccountRepository accountRepository, IConfiguration configuration)
    {
        this.accountRepository = accountRepository;
        this.configuration = configuration;
    }

    public IActionResult Index()
    {
        var account = accountRepository.GetAccountEmployee();
        return View(account);
    }


    // GET : Account/Register
    public IActionResult Register()
    {
        var genders = new List<SelectListItem>{
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            },
        };
        ViewBag.Genders = genders;
        return View();
    }

    // POST : Account/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterVM registerVM)
    {
        if (ModelState.IsValid)
        {
            var result = accountRepository.Register(registerVM);
            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        return View();
    }

    // GET : Account/Login
    public IActionResult Login()
    {
        return View();
    }

    // POST : Account/Login
    // Parameter LoginVM {Email, Password}
    // Validasi Email exist?, Password equal?
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginVM loginVM)
    {
        if (accountRepository.Login(loginVM))
        {
            var userdata = accountRepository.GetUserdata(loginVM.Email);
            var roles = accountRepository.GetRolesByNIK(loginVM.Email);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userdata.Email),
                new Claim(ClaimTypes.Name, userdata.FullName)
            };

            foreach (var item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signIn
                );

            var generateToken = new JwtSecurityTokenHandler().WriteToken(token);

            HttpContext.Session.SetString("jwtoken", generateToken);

            /*HttpContext.Session.SetString("email", userdata.Email);
            HttpContext.Session.SetString("fullname", userdata.FullName);
            HttpContext.Session.SetString("role", userdata.Role);*/

            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Account or Password Not Found!");
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction(nameof(Index), "Home");
    }
}