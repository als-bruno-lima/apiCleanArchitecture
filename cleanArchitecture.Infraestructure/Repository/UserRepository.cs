using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace cleanArchitecture.Infraestructure.Repository
{
    public class UserRepository : IUserRepository
    {

        private readonly IApplicationContext _context;
        public UserRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(User user)
        {
            try
            {

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error registering user", ex);
            }

        }

        public async Task<User> FindUser(string email, string password)
        {
            try
            {
                Console.WriteLine(email + password);
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            }
            catch (Exception ex)
            {
                throw new Exception("Error finding user by email", ex);
            }


        }
    }
}
