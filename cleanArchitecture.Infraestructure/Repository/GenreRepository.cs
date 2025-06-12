using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
namespace cleanArchitecture.Infraestructure.Repository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IApplicationContext _context;

        public GenreRepository(IApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetGenres()
        {
            try
            {
                return await _context.Genres.ToListAsync();
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
                return await _context.Genres.FindAsync(id);
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
                await _context.Genres.AddAsync(genre);
                await _context.SaveChangesAsync();
                return genre;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding genre", ex);
            }
        }
    }
}
