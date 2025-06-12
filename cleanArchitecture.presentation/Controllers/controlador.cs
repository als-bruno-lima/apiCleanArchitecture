using cleanArchitecture.Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace cleanArchitecture.presentation.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class controlador : ControllerBase
    {

        private readonly IMovieService _movieService;
        public controlador(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("movies")]
        public IActionResult Index()
        {
        var movies = _movieService.GetAllMovies();
           return Ok(movies);


        }
    }
}
