using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using Microsoft.AspNetCore.Mvc;
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
        var profillings = context.Profillings.ToList();
        return View(profillings);
    }
    public IActionResult Details(int id)
    {
        var profillings = context.Profillings.Find(id);
        return View(profillings);
    }

    public IActionResult Create()
    {
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
