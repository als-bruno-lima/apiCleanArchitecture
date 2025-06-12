using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.Service
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository) {
            _genreRepository = genreRepository;
        }


        public async Task<List<Genre>> GetGenres()
        {
            try
            {
                return await _genreRepository.GetGenres();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting genres", ex);
            }
            }

        public async Task<Genre> GetGenreById(int id)
        {
            try
            {
                return await _genreRepository.GetGenreById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting genre with id {id}", ex);
            }
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            try
            {
                if (genre == null)
                {
                    throw new ArgumentNullException("Genre cannot be null");
                }
                return await _genreRepository.AddGenre(genre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding genre", ex);
            }
        }

    }
}
