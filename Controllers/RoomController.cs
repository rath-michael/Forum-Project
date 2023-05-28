using Forum_API_Provider.Models.ForumModels.Rooms;
using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpPost]
        [Route("AddRoom")]
        public async Task<IActionResult> AddRoom(AddRoomRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(request);
            }


            return Ok();
        }
        // Update
        [Authorize]
        [HttpPut]
        [Route("UpdateRoom")]
        public async Task<IActionResult> UpdateRoom(Room originalRoom)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(originalRoom);
            }

            var updatedRoom = await repository.GetRoom(originalRoom.RoomId);
            if (updatedRoom == null)
            {
                return NotFound();
            }


            return Ok();
        }
        // Delete
        [Authorize]
        [HttpDelete]
        [Route("Delete/{roomId}")]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            return Ok();
        }
    }
}
