using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Controllers;

public class EducationController : Controller
{
    private readonly MyContext context;
    private readonly EducationRepository educationrepository;
    private readonly UniversityRepository universityRepository;

    public EducationController(MyContext context, EducationRepository educationrepository, UniversityRepository universityRepository)
    {
        this.context = context;
        this.educationrepository = educationrepository;
        this.universityRepository = universityRepository;
    }

    public IActionResult Index()
    {
        var education = educationrepository.GetEducationUniversities();
        return View(education);
    }
    public IActionResult Details(int id)
    {
        return View(educationrepository.GetEducationById(id));
    }

    public IActionResult Create()
    {
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(EducationUniversityVM education)
    {
        var result = educationrepository.Insert(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        if (result > 0)
            return RedirectToAction(nameof(Index));

        return View();
    }

    public IActionResult Edit(int id)
    {
        
        var universities = universityRepository.GetAll()
            .Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            });
        ViewBag.University = universities;
        var education = educationrepository.GetById(id);
        return View(new EducationUniversityVM
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityName = context.Universities.Find(education.UniversityId).Name
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EducationUniversityVM education)
    {

        var result = educationrepository.Update(new Education
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityId = Convert.ToInt16(education.UniversityName)
        });
        if (result > 0)
        {
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    public IActionResult Delete(int id)
    {
        var education = educationrepository.GetById(id);
        return View(education);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int id)
    {
        var result = educationrepository.Delete(id);
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
