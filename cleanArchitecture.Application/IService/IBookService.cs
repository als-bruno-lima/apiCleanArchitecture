using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.IService
{
    public interface IBookService
    {
        Task<List<Book>> GetBook(string? title = null, string? ISBN = null, int? authorId = 0, int? genreId = 0);
        Task<Book> GetBookById(int id);
        Task<Book> AddBook(BookDto bookDto);
        Task DeleteBook(int id);
        Task<Book> UpdateBook(int bookId, BookDto bookDto);
    }
}
