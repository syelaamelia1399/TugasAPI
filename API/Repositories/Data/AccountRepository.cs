using API.Context;
using API.Handlers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.NetworkInformation;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        private readonly MyContext _context;

        public AccountRepository(MyContext context)
        {
            _context = context;
        }

        public int Login(string email, string password)
        {
            var data = _context.Users
            .Include(x => x.Employee)
            .Include(x => x.Role)
            .SingleOrDefault(x => x.Employee.Email.Equals(email));  //pake hash, langkah awal cek email ada apa gk

            var result = Hashing.ValidatePassword(password, data.Password);
            if (result)
            {
                if (data != null)
                {                    
                    return 1;
                }
                return 2;
            }
            return 3;
        }


        public int Register(string fullName, string email, DateTime birthDate, string password)
        {
            Employee employee = new Employee()
            {
                FullName = fullName,
                Email = email,
                BirthDate = birthDate
            };
            var data = _context.Employees
                .SingleOrDefault(x => x.Email.Equals(email));
            if (data != null)
            {
                return 1;
            }
            else
            {
                _context.Employees.Add(employee);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    var id = _context.Employees.SingleOrDefault(x => x.Email.Equals(email)).Id;
                    User user = new User()
                    {
                        Id = id,
                        // Hashing
                        Password = Hashing.HashPassword(password),
                        RoleId = 1
                    };
                    _context.Users.Add(user);
                    var resultUser = _context.SaveChanges();
                    if (resultUser > 0)
                        return 2;
                }
            }
            return 3;
        }

        public int ChangePassword(string email, string password, string newPassword)
        {
            var data = _context.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            var result = Hashing.ValidatePassword(password, data.Password);
            if (result)
            {
                if (data != null)
                {
                    data.Password = Hashing.HashPassword(newPassword);
                    _context.Entry(data).State = EntityState.Modified;

                    var resultPassword = _context.SaveChanges();
                    if (resultPassword > 0)
                    {
                        return 1;
                    }
                }
                return 2;
            }
            return 3;
        }

        public int ForgetPassword(string email, string newPassword)
        {
            var data = _context.Users
                .Include(x => x.Employee)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data != null)
            {
                data.Password = Hashing.HashPassword(newPassword);
                _context.Entry(data).State = EntityState.Modified;

                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return 1;
                }
            }
            return 2;
        }
    }
}
