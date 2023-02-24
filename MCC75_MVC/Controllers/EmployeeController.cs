using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class EmployeeController : Controller
{
    private readonly MyContext context;
    private readonly EmployeeRepository employeeRepository;

    public EmployeeController(MyContext context, EmployeeRepository employeeRepository)
    {
        this.context = context;
        this.employeeRepository = employeeRepository;
    }

    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employees = employeeRepository.GetAll()
            .Select(e => new EmployeeVM
            {
                NIK = e.NIK,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Birthdate = e.Birthdate,
                Gender = (GenderEnum)e.Gender,
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,

            }).ToList();
        return View(employees);
    }
    public IActionResult Details(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employees = employeeRepository.GetById(id);
        return View(new EmployeeVM
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Birthdate = employees.Birthdate,
            Gender = (GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
    }

    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employess = employeeRepository.GetAll();
            /*.Select(e => new SelectListItem
            {
                Value = e.Gender.ToString(),
            });*/
        ViewBag.Gender = employess;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employees)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Insert(new Employee
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Birthdate = employees.Birthdate,
            Gender = (Employee.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employess = employeeRepository.GetAll()
            .Select(e => new SelectListItem
            {
                Value = e.Gender.ToString(),
            });
        ViewBag.Gender = employess;
        var employees = employeeRepository.GetById(id);
        return View(new EmployeeVM
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Birthdate = employees.Birthdate,
            Gender = (GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeVM employees)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Update(new Employee
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Birthdate = employees.Birthdate,
            Gender = (Employee.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string id)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var employees = employeeRepository.GetById(id);
        return View(employees);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string nik)
    {
        if (HttpContext.Session.GetString("email") == null)
        {
            return RedirectToAction("Unauthorized", "Error");
        }
        if (HttpContext.Session.GetString("role") != "Admin")
        {
            return RedirectToAction("Forbidden", "Error");
        }
        var result = employeeRepository.Delete(nik);
        if (result == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();

    }
}
