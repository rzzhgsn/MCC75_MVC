using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Repositories;

public class EmployeeRepository : IRepository<string, Employee>
{
    private readonly MyContext context;

    public EmployeeRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(string key)
    {
        int result = 0;
        var roles = GetById(key);
        if (roles == null)
        {
            return result;
        }

        context.Remove(roles);
        result = context.SaveChanges();

        return result;
    }

    public List<Employee> GetAll()
    {
        return context.Employees.ToList();
    }

    public Employee GetById(string key)
    {
        return context.Employees.Find(key);
    }

    public int Insert(Employee entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Employee entity)
    {

        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }

}
