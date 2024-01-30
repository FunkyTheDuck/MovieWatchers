using Microsoft.AspNetCore.Mvc;

namespace ApiMovieWatchers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MWUserController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOneUser(int id)
        {
            return Ok(id);
        }
    }
}
