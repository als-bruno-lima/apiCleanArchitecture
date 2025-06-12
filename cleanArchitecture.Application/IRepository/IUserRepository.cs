using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IRepository
{
    public interface IUserRepository
    {

        Task RegisterUser(User user);
        Task<User> FindUser(string email, string password);
    }
}
