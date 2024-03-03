using Microsoft.AspNetCore.JsonPatch;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetUsersAsync();
        Task<Users?> GetUserAsync(int userId);
        Task<Users> Login(string email, string password);
        Task<Users> AddUsers(UsersCreationDTO users);
        Task<Users> UpdateUsersAsync(int userId, UsersCreationDTO users);
        void DeleteUser(int userId);
        Task<Users> PatchUserDetails(int userId, JsonPatchDocument<UsersCreationDTO> userDetails);
    }
}
