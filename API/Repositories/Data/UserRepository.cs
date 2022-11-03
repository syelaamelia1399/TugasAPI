using API.Context;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class UserRepository
    {
        private readonly MyContext _context;

        public UserRepository(MyContext context)
        {
            _context = context;
        }

        // GET All
        public IEnumerable<User> Get()
        {
            return _context.Users.ToList();
        }

        // GET By Id
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        // CREATE
        public int Create(User user)
        {
            _context.Users.Add(user);
            var result = _context.SaveChanges();
            return result;
        }

        // UPDATE
        public int Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            var result = _context.SaveChanges();
            return result;
        }

        // DELETE
        public int Delete(int id)
        {
            var data = _context.Users.Find(id);
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
