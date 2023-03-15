using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API_Provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IForumRepository repository;
        public RoomController(IForumRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await repository.GetAllRooms());
        }
    }
}
