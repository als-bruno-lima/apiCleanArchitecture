using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IRepository
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetGenres();
        Task<Genre> GetGenreById(int id);
        Task<Genre> AddGenre(Genre genre);

    }
}
