using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class EmployeeController : Controller
{
    private readonly MyContext context;
    public EmployeeController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var result = context.Employees.
            Select(e => new EmployeeVM
            { 
                NIK = e.NIK,
                FirstName= e.FirstName,
                LastName= e.LastName,
                Bithdate= e.Bithdate,
                Gender = (EmployeeVM.GenderEnum)e.Gender,
                HiringDate = e.HiringDate,
                Email = e.Email,
                PhoneNumber= e.PhoneNumber,

            }).ToList();
        return View(result);
    }
    public IActionResult Details(string id)
    {
        var employees = context.Employees.Find(id);
        return View(new EmployeeVM
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Bithdate = employees.Bithdate,
            Gender = (EmployeeVM.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
    }

    public IActionResult Create()
    {
        var employess = context.Employees.ToList()
            .Select(e => new SelectListItem
            {
                Value = e.Gender.ToString(),
            });
        ViewBag.Employees = employess;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EmployeeVM employees)
    {
        context.Add(new Employee
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Bithdate = employees.Bithdate,
            Gender = (Employee.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(string id)
    {
        var employees = context.Employees.Find(id);
       /* var employess = context.Employees.ToList()
            .Select(e => new SelectListItem
            {
                Value = e.Gender.ToString(),
            });
        ViewBag.Employees = employess;*/
        return View(new EmployeeVM
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Bithdate = employees.Bithdate,
            Gender = (EmployeeVM.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EmployeeVM employees)
    {
        context.Entry(new Employee
        {
            NIK = employees.NIK,
            FirstName = employees.FirstName,
            LastName = employees.LastName,
            Bithdate = employees.Bithdate,
            Gender = (Employee.GenderEnum)employees.Gender,
            HiringDate = employees.HiringDate,
            Email = employees.Email,
            PhoneNumber = employees.PhoneNumber,
        }).State = EntityState.Modified;
        var result = context.SaveChanges();

        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(string id)
    {
        var employees = context.Employees.Find(id);
        return View(employees);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(string nik)
    {
        var employees = context.Employees.Find(nik);
        context.Remove(employees);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
