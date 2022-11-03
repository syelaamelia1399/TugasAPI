using API.Context;
using API.Handlers;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyContext _context;

        public AccountController(MyContext _context)
        {
            this._context = _context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public IActionResult Login(string email, string password)
        {
            try
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
                        HttpContext.Session.SetInt32("Id", data.Id);
                        HttpContext.Session.SetString("FullName", data.Employee.FullName);
                        HttpContext.Session.SetString("Email", data.Employee.Email);
                        HttpContext.Session.SetString("Role", data.Role.Name);

                        return Ok(new
                        {
                            StatusCode = 200,
                            Messege = "Data Ada",
                            Data = data.Employee.FullName, data.Employee.Email, data.Role.Name
                        });
                    }
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Tidak Ada"
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public IActionResult Register(string fullName, string email, DateTime birthDate, string password)
        {
            try
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
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal untuk Register"
                    });
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
                            return Ok(new
                            {
                                StatusCode = 200,
                                Messege = "Berhasil untuk Register"
                            });
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(string email, string password, string newPassword)
        {
            try
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
                            return Ok(new
                            {
                                StatusCode = 200,
                                Messege = "Berhasil untuk Change Password"
                            });
                        }
                    }
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal untuk Change Password"
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(string email, string newPassword)
        {
            try
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
                        return Ok(new
                        {
                            StatusCode = 200,
                            Messege = "Berhasil untuk Forget Password"
                        });
                    }
                }
                return Ok(new
                {
                    StatusCode = 200,
                    Messege = "Gagal untuk Forget Password"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }
    }
}
