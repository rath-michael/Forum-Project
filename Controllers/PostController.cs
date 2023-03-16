using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Forum_API_Provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PostController : BaseAPIController
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
            return NoContent();
        }
        // Add - Post
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(AddPostRequest request)
        {
            var post = new Post { UserId = UserID, DatePosted = DateTime.Now, Title = request.Title, Message = request.Message };
            var response = await repository.AddPost(post);

            if (response.Success)
            {
                return Ok(post);
            }
            return UnprocessableEntity(response);
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

            var updatedPost = await repository.UpdatePost(post, UserID);
            if (updatedPost.Success)
            {
                return Ok(updatedPost);
            }
            return UnprocessableEntity(updatedPost);
        }
        // Delete
        [HttpDelete]
        [Route("Delete/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var deleteAttempt = await repository.DeletePost(postId, UserID);

            if (deleteAttempt.Success)
            {
                return Ok(deleteAttempt);
            }
            return UnprocessableEntity(deleteAttempt);
        }
    }
}
