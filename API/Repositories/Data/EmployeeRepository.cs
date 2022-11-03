using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        private readonly MyContext _context;

        public EmployeeRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<Employee> Get()
        {
            return _context.Employees.ToList();
        }

        // GET By Id
        public Employee GetById(int id)
        {
            return _context.Employees.Find(id);
        }

        // CREATE
        public int Create(Employee employee)
        {
            _context.Employees.Add(employee);
            var result = _context.SaveChanges();
            return result;
        }

        // UPDATE
        public int Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        // DELETE
        public int Delete(int id)
        {
            var data = _context.Employees.Find(id);
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
