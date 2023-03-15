using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API_Provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IForumRepository repository;
        public PostController(IForumRepository repository)
        {
            this.repository = repository;

        }
        [HttpGet]
        [Route("GetAllPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await repository.GetAllPosts());
        }
    }
}
