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
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("/books")]
        [Authorize]
        public async Task<IActionResult> GetBooks(string? title = null, string? ISBN = null, int? authorId=0, int? genreId=0)
        {
            try
            {
                var books = await _bookService.GetBook(title,ISBN,authorId,genreId);
                return Ok(books);
            }
            catch (Exception ex)
            {
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
                    return NotFound($"Book with id {id} not found");
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/book")]
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
            return Ok($"Book with id {Id} deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            try { 
               await _bookService.UpdateBook(id, bookDto);
                return Ok($"Book with id {id} updated successfully");   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
}
}
