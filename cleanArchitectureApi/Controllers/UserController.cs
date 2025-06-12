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
    public class UserController:ControllerBase
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("users")]
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

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/login")]
        public async Task<IActionResult> LoginUser(string email, string password)
        {
            try
            {
                var response = await _userService.LoginUser(email, password);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
