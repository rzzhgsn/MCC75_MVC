using MCC75_MVC.Contexts;
using MCC75_MVC.Models;
using MCC75_MVC.Repositories.Interface;
using MCC75_MVC.ViewModels;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace MCC75_MVC.Repositories;

public class AccountRepository : IRepository<int, Account>
{
    private readonly MyContext context;
    private readonly EmployeeRepository employeeRepository;
    private readonly AccountRepository accountRepository;

    public AccountRepository(MyContext context, EmployeeRepository employeeRepository)
    {
        this.context = context;
        this.employeeRepository = employeeRepository;
        this.accountRepository = accountRepository;
    }
    public int Delete(int key)
    {
        throw new NotImplementedException();
    }

    public List<Account> GetAll()
    {
        return context.Accounts.ToList();
    }

    public Account GetById(int key)
    {
        throw new NotImplementedException();
    }

    public int Insert(Account entity)
    {
        throw new NotImplementedException();
    }

    public int Update(Account entity)
    {
        throw new NotImplementedException();
    }

    public int Register(RegisterVM registerVM)
    {
        int result = 0;
        University university = new University
        {
            Name = registerVM.UniversityName
        };

        // Bikin kondisi untuk mengecek apakah data university sudah ada
        if (context.Universities.Any(u => u.Name == university.Name))
        {
            university.Id = context.Universities
                .FirstOrDefault(u => u.Name == university.Name)
                .Id;
        }
        else
        {
            context.Universities.Add(university);
            result = context.SaveChanges();
        }

        Education education = new Education
        {
            Major = registerVM.Major,
            Degree = registerVM.Degree,
            GPA = registerVM.GPA,
            UniversityId = university.Id
        };
        context.Educations.Add(education);
        result = context.SaveChanges();

        Employee employee = new Employee
        {
            NIK = registerVM.NIK,
            FirstName = registerVM.FirstName,
            LastName = registerVM.LastName,
            Birthdate = registerVM.BirthDate,
            Gender = (Employee.GenderEnum)registerVM.Gender,
            HiringDate = registerVM.HiringDate,
            Email = registerVM.Email,
            PhoneNumber = registerVM.PhoneNumber,
        };
        context.Employees.Add(employee);
        result = context.SaveChanges();

        Account account = new Account
        {
            EmployeeNIK = registerVM.NIK,
            Password = registerVM.Password
        };
        context.Accounts.Add(account);
        result = context.SaveChanges();

        AccountRole accountRole = new AccountRole
        {
            AccountNIK = registerVM.NIK,
            RoleId = 2
        };

        context.AccountRoles.Add(accountRole);
        result = context.SaveChanges();

        Profilling profiling = new Profilling
        {
            EmployeeNIK = registerVM.NIK,
            EducationId = education.Id
        };
        context.Profillings.Add(profiling);
        result = context.SaveChanges();

        return result;
    }

    public bool Login(LoginVM loginVM)
    {
        var getAccounts = context.Employees.Join(
            context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new LoginVM
            {
                Email = e.Email,
                Password = a.Password
            });

        return getAccounts.Any(e => e.Email == loginVM.Email && e.Password == loginVM.Password);
    }

    public List<Account> GetAccountEmployee()
    {
        var result = (from e in employeeRepository.GetAll()
                      join a in GetAll()
                      on e.NIK equals a.EmployeeNIK
                      select new Account
                      {
                          Password = a.Password,
                          EmployeeNIK = e.Email,

                      }).ToList();
        return result;
    }

    public UserdataVM GetUserdata(string email)
    {
        /*var userdataMethod = context.Employees
            .Join(context.Accounts,
            e => e.NIK,
            a => a.EmployeeNIK,
            (e, a) => new { e, a })
            .Join(context.AccountRoles,
            ea => ea.a.EmployeeNIK,
            ar => ar.AccountNIK,
            (ea, ar) => new { ea, ar })
            .Join(context.Roles,
            eaar => eaar.ar.RoleId,
            r => r.Id,
            (eaar, r) => new UserdataVM
            {
                Email = eaar.ea.e.Email,
                FullName = String.Concat(eaar.ea.e.FirstName, eaar.ea.e.LastName),
                Role = r.Name
            }).FirstOrDefault(u => u.Email == email);*/

        // Query Syntax
        var userdata = (from e in context.Employees
                        join a in context.Accounts
                        on e.NIK equals a.EmployeeNIK
                        join ar in context.AccountRoles
                        on a.EmployeeNIK equals ar.AccountNIK
                        join r in context.Roles
                        on ar.RoleId equals r.Id
                        where e.Email == email
                        select new UserdataVM
                        {
                            Email = e.Email,
                            FullName = String.Concat(e.FirstName, " ", e.LastName),
                            Role = r.Name
                        }).FirstOrDefault();

        return userdata;
    }

    /*public Account GetEducationById(int key)
    {
        var account = GetById(key);
        var results = new Account
        {
            Password = account.Password,
            EmployeeNIK = e.Email,
        };
        return results;
    }*/
}
