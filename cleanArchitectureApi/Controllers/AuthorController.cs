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
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService, ILogger<AuthorController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAuthors()
        {
            try
            {
                var authors = _authorService.GetAuthors();
                _logger.LogInformation("Fetching all authors from the database");
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching authors");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorDto author)
        {
            try
            {
                if (author == null)
                {
                    _logger.LogWarning("Attempted to add a null author");
                    return BadRequest("Author cannot be null");
                }
                _logger.LogInformation("Adding a new author: {AuthorName}", author.Name);
                await _authorService.AddAuthor(author);


                return Ok($"Author {author.Name} created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding author");
                return BadRequest(ex.Message);
            }
        }
    }
}
