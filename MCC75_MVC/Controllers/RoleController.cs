using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace MCC75_MVC.Controllers;

public class RoleController : Controller
{
    private readonly MyContext context;
    public RoleController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var roles = context.Roles.ToList();
        return View(roles);
    }
    public IActionResult Details(int id)
    {
        var roles = context.Roles.Find(id);
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Role roles)
    {
        context.Add(roles);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var roles = context.Roles.Find(id);
        return View(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role roles)
    {
        context.Entry(roles).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var roles = context.Roles.Find(id);
        return View(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var university = context.Universities.Find(id);
        context.Remove(university);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
