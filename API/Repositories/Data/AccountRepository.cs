using API.Context;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        private readonly MyContext _context;

        public AccountRepository(MyContext context)
        {
            _context = context;
        }
    }
}
