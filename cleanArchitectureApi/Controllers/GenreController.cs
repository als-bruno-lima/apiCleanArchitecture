using cleanArchitecture.Application.Dto;
using cleanArchitecture.Application.IService;
using cleanArchitecture.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitectureApi.Controllers
{
    [ApiController]
    [Route("/Genre")]
    public class GenreController:ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService) {
            _genreService = genreService;
        }

        [HttpGet("genres")]
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
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("genres")]
        [Authorize]

        public async Task<IActionResult> AddGenre([FromBody] GenreDto genre) {
            try
            {
                var response = await _genreService.AddGenre(new cleanArchitecture.Domain.Genre
                {
                    Name = genre.Name,
                    Description = genre.Description

                });
                return Ok($"Genre {response.Name} created with id {response.Id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
