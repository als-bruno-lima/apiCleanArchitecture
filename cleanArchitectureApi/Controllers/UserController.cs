using System.Security.Cryptography;
using System.Text;
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IService;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace cleanArchitectureApi.Controllers
{
    [ApiController]
    [Route("User")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserDto user)
        {
            try
            {
                var newUser = new UserDto
                {
                    Password = user.Password,
                    Email = user.Email,
                    Name = user.Name,
                    status = user.status
                };
                await _userService.RegisterUser(newUser);
                _logger.LogInformation($"User {user.Name} registered successfully with email {user.Email}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser(string email, string password)
        {
            try
            {
                var response = await _userService.LoginUser(email, password);
                _logger.LogInformation($"User {email} logged in successfully");
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error logging in user");
                return BadRequest(ex.Message);
            }
        }

    }
}
