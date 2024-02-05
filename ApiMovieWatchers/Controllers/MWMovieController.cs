using Microsoft.AspNetCore.Mvc;
using MyApiModels;

namespace ApiMovieWatchers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MWMovieController : ControllerBase
    {
        public MWMovieController()
        {
            
        }
        [HttpPost]
        public IActionResult SaveMovie(Movie movie)
        {
            return Ok();
        }
    }
}
