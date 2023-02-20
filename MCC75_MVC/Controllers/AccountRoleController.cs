using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class AccountRoleController : Controller
{
    private readonly MyContext context;
    public AccountRoleController(MyContext context)
    {
        this.context = context;
    }

    public IActionResult Index()
    {
        var accountroles = context.AccountRoles.ToList();
        return View(accountroles);
    }
    public IActionResult Details(int id)
    {
        var accountroles = context.AccountRoles.Find(id);
        return View(accountroles);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AccountRole accountroles)
    {
        context.Add(accountroles);
        var result = context.SaveChanges();
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var accountroles = context.AccountRoles.Find(id);
        return View(accountroles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AccountRole accountroles)
    {
        context.Entry(accountroles).State = EntityState.Modified;
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var accountroles = context.AccountRoles.Find(id);
        return View(accountroles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var accountroles = context.AccountRoles.Find(id);
        context.Remove(accountroles);
        var result = context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }
}
