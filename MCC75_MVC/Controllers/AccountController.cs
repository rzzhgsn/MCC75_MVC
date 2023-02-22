using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MCC75_MVC.Controllers;

public class AccountController : Controller
{
    private readonly MyContext context;

    public AccountController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        /*var accounts = context.Accounts.ToList();
        return View(accounts);*/
        var result = context.Accounts.Join(
            context.Employees,
            a => a.EmployeeNIK,
            e => e.NIK,
            (a, e) => new Account
            {
                Password = a.Password,
                EmployeeNIK = e.Email
            });
        return View(result);
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
            University university = new University
            { 
                Name = registerVM.UniversityName
                
            };

            // Bikin kondisi untuk mengecek apakah data university sudah ada
            if (context.Universities.Any(u => u.Name == registerVM.UniversityName))
            {
                university.Id = context.Universities.FirstOrDefault(u => u.Name == university.Name).Id;
            }
            else
            {
                context.Universities.Add(university);
                context.SaveChanges();

            }

            Education education = new Education
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            context.Educations.Add(education);
            context.SaveChanges();

            Employee employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Birthdate = registerVM.BirthDate,
                Gender = (Employee.GenderEnum)registerVM.Gender,
                HiringDate = registerVM.HiringDate,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
            };
            context.Employees.Add(employee);
            context.SaveChanges();

            Account account = new Account
            {
                EmployeeNIK = registerVM.NIK,
                Password = registerVM.Password
            };
            context.Accounts.Add(account);
            context.SaveChanges();

            AccountRole accountRole = new AccountRole
            {
                AccountNIK = registerVM.NIK,
                RoleId = 2
            };

            context.AccountRoles.Add(accountRole);
            context.SaveChanges();

            Profilling profiling = new Profilling
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id
            };
            context.Profillings.Add(profiling);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
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
        var getAccounts = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password

            });
           
        if (getAccounts.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password))
        {
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Account or Password Not Found");
        return View();
    }

}