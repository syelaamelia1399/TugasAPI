using API.Context;
using API.Models;
using API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class RoleRepository : IRepository<Role, int>
    {
        private readonly MyContext _context;

        public RoleRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<Role> Get()
        {
            return _context.Roles.ToList();
        }

        // GET By Id
        public Role GetById(int id)
        {
            return _context.Roles.Find(id);
        }

        // CREATE
        public int Create(Role role)
        {
            _context.Roles.Add(role);
            var result = _context.SaveChanges();
            return result;
        }

        // UPDATE
        public int Update(Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        // DELETE
        public int Delete(int id)
        {
            var data = _context.Roles.Find(id);
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
