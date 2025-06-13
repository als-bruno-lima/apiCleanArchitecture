using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitectureApi.Controllers
{
    [ApiController]
    [Route("Books")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookService _bookService;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetBooksWithFilter(string? title = null, string? ISBN = null, int? authorId = 0, int? genreId = 0)
        {
            try
            {
                var books = await _bookService.GetBook(title, ISBN, authorId, genreId);
                _logger.LogInformation($"Fetching books with filters: Title={title}, ISBN={ISBN}, AuthorId={authorId}, GenreId={genreId}");
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching books with filters");
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/books/{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookById(int id)
        {
            try
            {
                var book = await _bookService.GetBookById(id);
                if (book == null)
                {
                    _logger.LogWarning($"Book with id {id} not found");
                    return NotFound($"Book with id {id} not found");
                }

                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching book with id {id}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                {
                    return BadRequest("Book cannot be null");
                }

                var book = await _bookService.AddBook(bookDto);
                _logger.LogInformation($"Book {book.Title} created with id {book.Id}");
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            try
            {
                await _bookService.DeleteBook(Id);
                _logger.LogInformation($"Book with id {Id} deleted successfully");
                return Ok($"Book with id {Id} deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting book with id {Id}");
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            try
            {

                await _bookService.UpdateBook(id, bookDto);
                _logger.LogInformation($"Book with id {id} updated successfully");
                return Ok($"Book with id {id} updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating book with id {id}");
                return BadRequest(ex.Message);
            }

        }
    }
}
