using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace cleanArchitecture.Application.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task RegisterUser(UserDto user)
        {
            try
            {
                if (user == null)
                {
                    throw new ArgumentNullException("User cannot be null");
                }

                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("X2"));
                    }


                    var userdb = new User
                    {
                        Email = user.Email,
                        Password = await Task.FromResult(builder.ToString()),
                        Name = user.Name,
                        status = user.status,
                    };

                    await _userRepository.RegisterUser(userdb);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error registering user", ex);
            }
        }
        public async Task<string> LoginUser(string email, string password)
        {
            try
            {
                using (var sha256 = System.Security.Cryptography.SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        builder.Append(bytes[i].ToString("X2"));
                    }


                    var user = await _userRepository.FindUser(email, await Task.FromResult(builder.ToString()));

                    if (user == null) return "User not found";

                    var jwt = _configuration.GetSection("jwt").Get<JWT>();
                    var claims = new[]
                    {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("email", user.Email)
            };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.key));
                    var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



                    var token = new JwtSecurityToken(
                        jwt.issuer,
                        jwt.audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signin
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return tokenString;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error logging in user", ex);

            }
        }
    }
}
