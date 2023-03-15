using Forum_API_Provider.Models.DbModels;
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
        public async Task<IActionResult> GetRooms()
        {
            return Ok(await repository.GetAllRooms());
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await repository.GetAllPosts());
        }
    }
}
