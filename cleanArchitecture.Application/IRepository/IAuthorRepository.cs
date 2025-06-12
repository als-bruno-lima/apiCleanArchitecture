using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IRepository
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);

        Task AddAuthor(Author author);

    }
}
