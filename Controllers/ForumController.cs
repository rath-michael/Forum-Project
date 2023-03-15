using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API_Provider.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private IForumRepository repository;
        public ForumController(IForumRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [Route("GetAllRooms")]
        public IActionResult GetRooms()
        {
            var rooms = repository.GetAllRooms();
            if (rooms != null && rooms.Count > 0 )
            {
                return Ok(rooms);
            }
            return UnprocessableEntity();
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public IActionResult GetPosts()
        {
            var posts = repository.GetAllPosts();
            if (posts != null && posts.Count > 0 ) 
            { 
                return Ok(posts); 
            }
            return UnprocessableEntity();
        }
    }
}
