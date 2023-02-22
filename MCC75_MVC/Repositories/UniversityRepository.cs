using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Repositories;

public class UniversityRepository : IRepository<int, University>
{
    private readonly MyContext context;
    public UniversityRepository(MyContext context)
    {
        this.context = context;
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

    public List<University> GetAll()
    {
        return context.Universities.ToList() ?? null;
    }

    public University GetById(int key)
    {
        return context.Universities.Find(key) ?? null;
    }

    public int Insert(University entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(University entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }
}
