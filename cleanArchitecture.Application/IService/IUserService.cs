using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IService
{
    public interface IUserService
    {
        Task RegisterUser(UserDto user);
        Task<string> LoginUser(string email, string password);


    }
}
