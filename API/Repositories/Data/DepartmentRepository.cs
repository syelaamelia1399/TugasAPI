using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private readonly MyContext _context;

        public DepartmentRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<Department> Get()
        {
            return _context.Departments.ToList();
        }
      
        // GET By Id
        public Department GetById(int id)
        {
            return _context.Departments.Find(id);
        }

        // CREATE
        public int Create(Department department)
        {
            _context.Departments.Add(department);
            var result = _context.SaveChanges();
            return result;
        }

        // UPDATE
        public int Update(Department department)
        {
            _context.Entry(department).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        // DELETE
        public int Delete(int id)
        {
            var data = _context.Departments.Find(id);
            if (data != null)
            {
                _context.Remove(data);
                var result = _context.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
