using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

namespace cleanArchitecture.Infraestructure.Repository
{
    public class BookRepository:IBookRepository
    {
        private readonly IApplicationContext _applicationContext;


        public BookRepository(IApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Book>> GetBooks() {
            try
            {
                return await _applicationContext.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error getting books", ex);
            }

        }

        public async Task<Book> GetBookById(int id)
        {

            try
            {
                return await _applicationContext.Books.Include(b => b.Genre).Include(b=>b.Author).FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting book with id {id}", ex);
            }
        }

        public async Task<Book> AddBook(Book book)
        {
                try
                {
                var result = await _applicationContext.Books.AddAsync(book);
                await _applicationContext.SaveChangesAsync();
                return result.Entity;
                
            }
                catch (Exception ex)
                {
                    throw new Exception("Error adding book", ex);
                }
        }

        public async Task DeleteBook(int id)
        {
            try
            {
                var book = await GetBookById(id);
                 _applicationContext.Books.Remove(book);
                await _applicationContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting book", ex);
            }

        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                _applicationContext.Books.Update(book);
                await _applicationContext.SaveChangesAsync();
                return book;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating book", ex);
            }
        }

        }
}
