using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API_Provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IForumRepository repository;
        public RoomController(IForumRepository repository)
        {
            this.repository = repository;
        }
        // Get all
        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await repository.GetAllRooms());
        }
        // Get
        [HttpGet]
        [Route("GetRoom/{roomId}")]
        public async Task<IActionResult> GetRoom(int roomId)
        {
            var room = await repository.GetRoom(roomId);
            if (room != null)
            {
                return Ok(room);
            }
            return NotFound();
        }
        // Add
        // Update
        // Delete
    }
}
