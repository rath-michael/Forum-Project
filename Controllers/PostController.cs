using Forum_API_Provider.Models.ForumModels;
using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forum_API_Provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IForumRepository repository;
        public PostController(IForumRepository repository)
        {
            this.repository = repository;

        }
        // Get all
        [HttpGet]
        [Route("GetAllPosts")]
        public async Task<IActionResult> GetPosts()
        {
            return Ok(await repository.GetAllPosts());
        }
        // Get
        [HttpGet]
        [Route("GetPost/{postId}")]
        public async Task<IActionResult> GetPost(int postId)
        {
            var post = await repository.GetPost(postId);
            if (post != null)
            {
                return Ok(post);
            }
            return NotFound();
        }
        // Add - Post
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addedPost = await repository.AddPost(post);
            if (addedPost != null)
            {
                return Ok(addedPost);
            }

            return UnprocessableEntity(post);
        }
        // Update - Put
        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPost = await repository.UpdatePost(post);
            if (updatedPost != null)
            {
                return Ok(updatedPost);
            }
            return UnprocessableEntity(post);
        }
        // Delete
        [HttpDelete]
        [Route("Delete/{postId")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var post = await repository.GetPost(postId);
            if (post == null)
            {
                return NotFound();
            }

            repository.DeletePost(post);
            return Ok();
        }
    }
}
