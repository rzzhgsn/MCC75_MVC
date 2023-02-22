using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Repositories;

public class EducationRepository : IRepository<int, Education>
{
    private readonly MyContext context;
    private readonly UniversityRepository universityRepository;

    public EducationRepository(MyContext context, UniversityRepository universityRepository)
    {
        this.context = context;
        this.universityRepository = universityRepository;
    }
    public int Delete(int key)
    {
        int result = 0;
        var university = GetById(key);
        if (university == null)
        {
            return result;
        }

        context.Remove(university);
        result = context.SaveChanges();

        return result;
    }

    public List<Education> GetAll()
    {
        return context.Educations.ToList();
    }

    public Education GetById(int key)
    {
        return context.Educations.Find(key);
    }

    public int Insert(Education entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Education entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }

    public List<EducationUniversityVM> GetEducationUniversities()
    {
        var results = (from e in GetAll()
                       join u in universityRepository.GetAll()
                       on e.UniversityId equals u.Id
                       select new EducationUniversityVM
                       {
                           Id = e.Id,
                           Degree = e.Degree,
                           GPA = e.GPA,
                           Major = e.Major,
                           UniversityName = u.Name,
                       }).ToList();
        return results;
    }

    public EducationUniversityVM GetEducationById(int key)
    {
        var education = GetById(key);
        var results = new EducationUniversityVM
        {
            Id = education.Id,
            Degree = education.Degree,
            GPA = education.GPA,
            Major = education.Major,
            UniversityName = context.Universities.Find(education.UniversityId).Name
        };
        return results;
    }

    
}
