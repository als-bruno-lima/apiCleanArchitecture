using cleanArchitecture.Application.IService;
using Microsoft.AspNetCore.Mvc;
using cleanArchitecture.Infraestructure.Dto;
using Microsoft.AspNetCore.Authorization;
namespace cleanArchitectureApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }



        [HttpGet("authors")]
        [Authorize]
        public IActionResult GetAuthors()
        {
            try
            {

                var authors = _authorService.GetAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("authors")]
        [Authorize]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDto author)
        {
            try
            {
                if (author == null)
                {
                    return BadRequest("Author cannot be null");
                }

                await _authorService.AddAuthor(author);


                return Ok($"Author {author.Name} created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
