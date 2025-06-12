using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IService
{

    public interface IUtilsService
    {
        Task<string> HashPassword(string text);
        Task<string> GenerateToken(User user);


    }
}
