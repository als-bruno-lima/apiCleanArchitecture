
using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IRepository;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Domain;

namespace cleanArchitecture.Application.Service
{
    public class BookService:IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }


        public async Task<List<Book>> GetBook(string? title, string? ISBN, int? authorId, int? genreId)
        {
            try
            {
                var books = await _bookRepository.GetBooks();

                if (!string.IsNullOrEmpty(title))
                {
                    books = books.Where(b => b.Title.Contains(title)).ToList();
                }

                if (!string.IsNullOrEmpty(ISBN))
                {
                    books = books.Where(b => b.ISBN.Equals(ISBN)).ToList();
                }

                if (authorId.HasValue && authorId.Value > 0)
                {
                    books = books.Where(b => b.AuthorId == authorId.Value).ToList();
                }
                if (genreId.HasValue && genreId.Value > 0)
                {
                    books = books.Where(b => b.GenreId == genreId.Value).ToList();
                }



                return books.ToList();
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
                return await _bookRepository.GetBookById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting book with id {id}", ex);
            }
        }



        public async Task<Book> AddBook(BookDto book)
        {

            try
            {
                var author = await _authorRepository.GetAuthorById(book.AuthorId);
                var genre = await _genreRepository.GetGenreById(book.GenreId);

                if (author == null)
                {
                    throw new Exception($"Author with id {book.AuthorId} not found");
                }
                else if (genre == null)
                {
                    throw new Exception($"Genre with id {book.GenreId} not found");
                }
                var newBook = new Book
                {
                    Title = book.Title,
                    Summary = book.Summary,
                    ReleaseYear = book.ReleaseYear,
                    ImageUrl = book.ImageUrl,
                    Stock = book.Stock,
                    ISBN = book.ISBN,
                    AuthorId = book.AuthorId,
                    GenreId = book.GenreId

                };
                return await _bookRepository.AddBook(newBook);

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

                await _bookRepository.DeleteBook(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting book with id {id}", ex);
            }
        }
        public async Task<Book> UpdateBook(int bookId, BookDto bookDto)
        {

            try
            {
                var book = await _bookRepository.GetBookById(bookId);
                if (book == null)
                {
                    throw new Exception($"Book with id {bookId} not found");
                }

                var author = await _authorRepository.GetAuthorById(book.AuthorId);
                var genre = await _genreRepository.GetGenreById(book.GenreId);
                if (author == null)
                {
                    throw new Exception($"Author with id {book.AuthorId} not found");
                }
                else if (genre == null)
                {
                    throw new Exception($"Genre with id {book.GenreId} not found");
                }
                book.Title = bookDto.Title;
                book.Summary = bookDto.Summary;
                book.ReleaseYear = bookDto.ReleaseYear;
                book.ImageUrl = bookDto.ImageUrl;
                book.Stock = bookDto.Stock;
                book.ISBN = bookDto.ISBN;
                book.AuthorId = bookDto.AuthorId;
                book.GenreId = bookDto.GenreId;


                return await _bookRepository.UpdateBook(book);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating book with id {bookId}", ex);
            }
        }

    }
}
