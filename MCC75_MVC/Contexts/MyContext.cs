using MCC75_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace MCC75_MVC.Contexts;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {

    }

    // Introduce Database 
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountRole> AccountRoles { get; set; }
    public DbSet<Education> Educations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Profilling> Profillings { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<University> Universities { get; set; }

    //Fluent API
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Membuat atribute menjadi uniqe
        /*modelBuilder.Entity<Employee>().HasAlternateKey(e => new
        {
            e.Email,
            e.PhoneNumber,
        });*/

        // Relasi one Employee ke one Account sekaligus menjadi Primary Key
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Account)
            .WithOne(a => a.Employee)
            .HasForeignKey<Account>(fk => fk.EmployeeNIK);
    }

}
