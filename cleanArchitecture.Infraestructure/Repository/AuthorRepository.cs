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
    public class AuthorRepository : IAuthorRepository
    {


        private readonly ApplicationContext _context;
        public AuthorRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<List<Author>> GetAuthors()
        {
            try
            {
                return _context.Authors.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting authors", ex);
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            try
            {
                return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting author with id {id}", ex);
            }
        }

        public async Task AddAuthor(Author author)
        {
            try
            {
                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding author", ex);
            }

        }
    }
}
