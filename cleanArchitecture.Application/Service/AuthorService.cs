using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Domain;
using cleanArchitecture.Infraestructure.Dto;

namespace cleanArchitecture.Application.Service
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<List<Author>> GetAuthors()
        {
            try
            {
                return await _authorRepository.GetAuthors();
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
                return await _authorRepository.GetAuthorById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting author with id {id}", ex);
            }
        }

        public async Task AddAuthor(AuthorDto authorDto)
        {
            try
            {
                if (authorDto == null)
                {
                    throw new ArgumentNullException("Author cannot be null");
                }

                var author = new Author
                {
                    Name = authorDto.Name,
                    Country = authorDto.Country,
                    Date = authorDto.Date,
                };

                await _authorRepository.AddAuthor(author);

            }
            catch (Exception ex)
            {
                throw new Exception("Error adding author", ex);

            }
        }

    }
}
