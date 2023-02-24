using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Repositories;

public class RoleRepository : IRepository<int, Role>
{
    private readonly MyContext context;

    public RoleRepository(MyContext context)
    {
        this.context = context;
    }
    public int Delete(int key)
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

    public List<Role> GetAll()
    {
        return context.Roles.ToList();
    }

    public Role GetById(int key)
    {
        return context.Roles.Find(key);
    }

    public int Insert(Role entity)
    {
        int result = 0;
        context.Add(entity);
        result = context.SaveChanges();
        return result;
    }

    public int Update(Role entity)
    {
        int result = 0;
        context.Entry(entity).State = EntityState.Modified;
        result = context.SaveChanges();

        return result;
    }
}
