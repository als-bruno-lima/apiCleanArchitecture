using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;
using cleanArchitecture.Infraestructure.Dto;


namespace cleanArchitecture.Application.IService
{
    public interface IAuthorService
    {

        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);

        Task AddAuthor(AuthorDto author);


    }
}
