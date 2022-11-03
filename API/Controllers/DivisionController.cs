using API.Models;
using API.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private DivisionRepository _repository;
        
        public DivisionController(DivisionRepository divisionRepository)
        {
            _repository = divisionRepository;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            /*
            var data = _repository.Get();
            return Ok(data);
            */
            try
            {
                var data = _repository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Tidak Ada"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Ada",
                        Data = data
                    });
                }
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

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            /*
            var data = _repository.GetById(id);
            if(data == null)
            {
                return Ok(new { Messege = "Data Tidak Ditemukan" });
            }
            return Ok(data);
            */
            try
            {
                var data = _repository.GetById(id);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Tidak Ditemukan"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Ada",
                        Data = data
                    });
                }
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
        public ActionResult Create(Division division)
        {
            /*
            var result = _repository.Create(division);
            if(result == 0)
            {
                return Ok(new { Messege = "Data Gagal Disimpan" });
            }
            return Ok(new { Messege = "Data Berhasil Disimpan" });
            */
            try
            {
                var result = _repository.Create(division);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Gagal Disimpan"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Berhasil Disimpan"
                    });
                }
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
        public ActionResult Update(Division division)
        {
            /*
            var result = _repository.Update(division);
            if(result == 0)
            {
                return Ok(new { Messege = "Data Gagal di Update" });
            }
            return Ok(new { Messege = "Data Berhasil di Update" });
            */
            try
            {
                var result = _repository.Update(division);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Gagal di Update"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Berhasil di Update"
                    });
                }
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

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            /*
            var result = _repository.Delete(id);
            if(result == 0)
            {
                return Ok(new { Messege = "Data Gagal di Hapus" });
            }
            return Ok(new { Messege = "Data Berhasil di Hapus" });
            */
            try
            {
                var result = _repository.Delete(id);
                if (result == 0)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Gagal di Hapus"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Data Berhasil di Hapus"
                    });
                }
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
