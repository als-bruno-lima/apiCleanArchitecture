using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitectureApi.Controllers
{
    [ApiController]
    [Route("/Genre")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly ILogger<GenreController> _logger;

        public GenreController(IGenreService genreService, ILogger<GenreController> logger)
        {
            _genreService = genreService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGenres()
        {
            try
            {
                var genres = await _genreService.GetGenres();
                return Ok(genres);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching genres");
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Authorize]

        public async Task<IActionResult> AddGenre([FromBody] GenreDto genre)
        {
            try
            {
                var response = await _genreService.AddGenre(new cleanArchitecture.Domain.Genre
                {
                    Name = genre.Name,
                    Description = genre.Description

                });
                _logger.LogInformation($"Genre {response.Name} created with id {response.Id}");
                return Ok($"Genre {response.Name} created with id {response.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding genre");
                return BadRequest(ex.Message);
            }
        }
    }
}
