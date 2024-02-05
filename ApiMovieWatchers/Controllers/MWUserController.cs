using ApiCLFacade;
using Microsoft.AspNetCore.Mvc;
using MyApiModels;

namespace ApiMovieWatchers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MWUserController : ControllerBase
    {
        IUserRepository userRepo;
        public MWUserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetOneUser(int id)
        {
            if(id != 0)
            {
                User user = await userRepo.GetOneUserAsync(id);
                if(user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
    }
}
