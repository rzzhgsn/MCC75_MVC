﻿using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class UniversityController : Controller
{
    private readonly UniversityRepository repository;

    public UniversityController(UniversityRepository repository)
    {
        this.repository = repository;
    }

    [Authorize(Roles = "Admin, User")]
    public IActionResult Index()
    {
        var universities = repository.GetAll();
        return View(universities);

    }

    [Authorize(Roles = "Admin, User")]
    public IActionResult Details(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(University university)
    {
        var result = repository.Insert(university);
        if (result > 0)
            return RedirectToAction(nameof(Index));
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Edit(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(University university)
    {
        var result = repository.Update(university);
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    [Authorize(Roles = "Admin")]
    public IActionResult Delete(int id)
    {
        var university = repository.GetById(id);
        return View(university);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = repository.Delete(id);
        if (result == 0)
        {
            // Data Tidak Ditemukan
        }
        else
        {
            return RedirectToAction(nameof(Index));

        }
        return View();
    }

}
