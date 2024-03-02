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

        [HttpPost("{id}")]
        public async Task<ActionResult<Users>> Login(int id, string email, string password)
        {
            var user = await UserRepository.Login(id, email, password);
            if (user == null)
            {
               // return NotFound("Email or Password is incorrect.");
                return StatusCode(400, "Email or Password is incorrect."); 
            }
            return Ok(user);
        }
    }
}
