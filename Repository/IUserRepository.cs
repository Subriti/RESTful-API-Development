using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users?> GetUserAsync(int userId);
        Task<Users> Login(int userId, string email, string password);
        Task<Users?> AddUsers(Users users);
        Task<Users> UpdateUsersAsync(int userId, Users users);
        Task<Users> DeleteUsersAsync(int userId);
        Task<Users> PatchUserDetails(int userId, Users userDetails);
    }
}
