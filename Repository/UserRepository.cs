using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.DbContext;
using RESTful_API__ASP.NET_Core.Models;
using RESTful_API__ASP.NET_Core.Profiles;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        public UserRepository(DBContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        public async Task<Users> Login(string email, string password)
        {
            var user = await _context.Users.Where(c => c.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                if (user.Email.Equals(email) && user.Password.Equals(password))
                {
                    return (user);
                }
            }
            return null;
        }

        public async Task<Users> AddUsers(UsersCreationDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);

            var user= _context.Users.Add(finalUser);
            await _context.SaveChangesAsync();

            return GetUserAsync(user.Entity.Id).Result;
        }

        public void DeleteUser(int userId)
        {
            var user = GetUserAsync(userId);
            _context.Users.Remove(user.Result);
            _context.SaveChangesAsync();
        }

        public async Task<Users> PatchUserDetails(int userId, JsonPatchDocument<UsersCreationDTO> patchDocument)
        {
            var existingUser = await GetUserAsync(userId);
            if (existingUser != null)
            {
                //transform user entity to usercreationDTO
                var userToPatch = new UsersCreationDTO(existingUser.Name,existingUser.Email,existingUser.Password,existingUser.CityId);

                patchDocument.ApplyTo(userToPatch);

                existingUser.Name = userToPatch.Name;
                existingUser.Email= userToPatch.Email;
                existingUser.Password = userToPatch.Password;
                existingUser.CityId= userToPatch.CityId;

                return existingUser;
            }
            return null;
        }    

        public async Task<Users> UpdateUsersAsync(int userId, UsersCreationDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);
            var existingUser = await GetUserAsync(userId);
            if (existingUser != null)
            {
                existingUser.Name = finalUser.Name;
                existingUser.Email = finalUser.Email;
                existingUser.CityId = finalUser.CityId;
                _context.SaveChanges();
                return existingUser;
            }
            return null;
        }
    }
}
