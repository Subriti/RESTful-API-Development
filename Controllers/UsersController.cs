using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Models;
using RESTful_API__ASP.NET_Core.Repository;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository UserRepository;
        public UsersController(IUserRepository userRepository) {
            UserRepository = userRepository ?? throw new ArgumentOutOfRangeException(nameof(userRepository));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return Ok( await UserRepository.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUser(int id)
        {
            var user= await UserRepository.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Users>> Login(string email, string password)
        {
            var user = await UserRepository.Login(email, password);
            if (user == null)
            {
               // return NotFound("Email or Password is incorrect.");
                return StatusCode(400, "Email or Password is incorrect."); 
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> AddUsers(UsersCreationDTO user)
        {
            var users = await UserRepository.AddUsers(user); 
            if(users == null)
            {
                return StatusCode(400, "User already exists.");
            }
            return Ok(users);
        }

        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            UserRepository.DeleteUser(userId);
            return NoContent();
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<Users>> UpdateUsers(int userId, UsersCreationDTO user)
        {
            var newUser = await UserRepository.UpdateUsersAsync(userId, user);
            if (newUser == null)
            {
                return BadRequest("Update failed");
            }
            return Ok(newUser);
        }

        [HttpPatch("{userId}")]
        public async Task<ActionResult<Users>> PatchUserDetails(int userId, JsonPatchDocument<UsersCreationDTO> patchDocument)
        {
            var user= await UserRepository.PatchUserDetails(userId, patchDocument);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(user))
            {
                return BadRequest(ModelState);
            }
            if (user == null)
            {
                NotFound();
            }
            return Ok(user);
        }
    }
}
