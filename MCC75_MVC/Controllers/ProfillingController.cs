using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class ProfillingController : Controller
{
    private readonly MyContext context;
    public ProfillingController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        /*var result = context.Profillings.Join(
            context.Employees,
            p => p.EmployeeNIK,
            e => e.NIK,
            (p, e) => new Profilling
            {
                EmployeeNIK = e.FirstName + " " + e.LastName,
                EducationId = p.EducationId
            });
        return View(result);*/
        return View();
    }
    public IActionResult Details(int id)
    {
        var profillings = context.Profillings.Find(id);
        return View(profillings);
    }

    public IActionResult Create()
    {
        /*var employees = context.Employees.ToList()
            .Select(e => new SelectListItem
            {
                Value = e.NIK.ToString(),
                Text = e.FirstName + " " + e.LastName
            });
        ViewBag.Employee = employees;
        var educations = context.Educations.ToList()
            .Select(ed => new SelectListItem
            {
                Value = ed.Id.ToString(),
                Text = ed.Major
            });
        ViewBag.Educations = educations;*/
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Profilling profillings)
    {
        context.Add(profillings);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var profillings = context.Profillings.Find(id);
        return View(profillings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Profilling profillings)
    {
        context.Entry(profillings).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var profillings = context.Profillings.Find(id);
        return View(profillings);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var profillings = context.Profillings.Find(id);
        context.Remove(profillings);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
