using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories;
using MCC75_MVC.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleRepository roleRepository;

    public RoleController(RoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public IActionResult Index()
    {
        var roles = roleRepository.GetAll();
        return View(roles);
    }
    public IActionResult Details(int id)
    {
        var roles = roleRepository.GetById(id);
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
        var result = roleRepository.Insert(roles);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    public IActionResult Edit(int id)
    {
        var roles = roleRepository.GetById(id);
        return View(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Role roles)
    {
        var result = roleRepository.Update(roles);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var roles = roleRepository.GetById(id);
        return View(roles);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = roleRepository.Delete(id);
        if (result == 0)
        {
            // Data Tidak Ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));

        }
        return RedirectToAction(nameof(Delete));
    }
}
