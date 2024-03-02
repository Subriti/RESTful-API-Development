using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.DbContext;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        public UserRepository(DBContext context)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
        }
        public async Task<Users?> GetUserAsync(int userId)
        {
            //returns only user detail
            //return await _context.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();

            //returns user + child details (city)
            return await _context.Users.Include(u => u.City).Where(u => u.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            //return await _context.Users.OrderBy(c => c.Name).ToListAsync();
            return await _context.Users.Include(u => u.City).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Users> Login(int userId, string email, string password)
        {
            var user = await _context.Users.Where(c => c.Id == userId).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                {
                    return (user);
                }
            }
            return null;
        }
        public Task<Users?> AddUsers(Users users)
        {
            throw new NotImplementedException();
        }

        public Task<Users> DeleteUsersAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Users> PatchUserDetails(int userId, Users userDetails)
        {
            throw new NotImplementedException();
        }

        public Task<Users> UpdateUsersAsync(int userId, Users users)
        {
            throw new NotImplementedException();
        }
    }
}
