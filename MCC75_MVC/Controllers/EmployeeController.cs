using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

[Authorize(Roles = "Admin")]
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
        var gender = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "0",
                Text = "Male"
            },
            new SelectListItem
            {
                Value = "1",
                Text = "Female"
            }
        };

        ViewBag.Gender = gender;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employees)
    {
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
        var employees = employeeRepository.GetById(id);
        return View(employees);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string nik)
    {
        var result = employeeRepository.Delete(nik);
        if (result == 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();

    }
}
