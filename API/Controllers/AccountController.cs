using API.Context;
using API.Handlers;
using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository _repository;

        public AccountController(AccountRepository accountRepository)
        {
            _repository = accountRepository;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var item = _repository.Login(email, password);

                return item switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Ada",
                    }),
                    2 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Tidak Ada"
                    }),
                    3 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal Login"
                    })

                };
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
        public ActionResult Register(string fullName, string email, DateTime birthDate, string password)
        {
            try
            {
                var item = _repository.Register(fullName, email, birthDate, password);

                return item switch
                {
                    1 => Ok(new
                        {
                        StatusCode = 200,
                        Messege = "Gagal Untuk Register",
                        }),
                    2 => Ok(new
                        {
                        StatusCode = 200,
                        Messege = "Berhasil Untuk Register"
                        }),
                    3 => Ok(new
                        {
                        StatusCode = 200,
                        Messege = "Gagal Register"
                    })

                };
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
        
        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(string email, string password, string newPassword)
        {
            try
            {
                var item = _repository.ChangePassword(email, password, newPassword);

                return item switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Berhasil Untuk Change Password",
                    }),
                    2 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal Untuk Change Password"
                    }),
                    3 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal Register"
                    })

                };
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

        [HttpPut]
        [ValidateAntiForgeryToken]
        [Route("ForgetPassword")]
        public ActionResult ForgetPassword(string email, string newPassword)
        {
            try
            {
                var item = _repository.ForgetPassword(email, newPassword);

                return item switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Berhasil Untuk Change Password",
                    }),
                    2 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Gagal Untuk Change Password"
                    })
                };
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



        // WEB APP YANG LAMA
        /*
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
                */
    }
}
