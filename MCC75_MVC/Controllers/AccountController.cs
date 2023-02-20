using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var accounts = context.Accounts.ToList();
        return View(accounts);
    }
    public IActionResult Details(int id)
    {
        var accounts = context.Accounts.Find(id);
        return View(accounts);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Account accounts)
    {
        context.Add(accounts);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var accounts = context.Accounts.Find(id);
        return View(accounts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Account accounts)
    {
        context.Entry(accounts).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var accounts = context.Accounts.Find(id);
        return View(accounts);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var accounts = context.Accounts.Find(id);
        context.Remove(accounts);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
