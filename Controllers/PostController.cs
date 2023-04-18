using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        // Get posts associated with specific room
        [HttpGet]
        [Route("GetPostByRoom/{roomId}")]
        public async Task<IActionResult> GetPostsByRoom(int roomId)
        {
            var room = await repository.GetRoom(roomId);
            if (room != null)
            {
                return Ok(await repository.GetPostsByRoom(room));
            }
            return NoContent();
        }
        // Get posts associated with specific user
        [HttpGet]
        [Route("GetPostsByUser/{userId}")]
        public async Task<IActionResult> GetPostsByUser(int userId)
        {
            var user = await repository.GetUser(userId);
            if (user != null)
            {
                return Ok(await repository.GetPostsByUser(user));
            }
            return NoContent();
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
        // Post
        [Authorize]
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(AddPostRequest request)
        {
            var post = new Post { 
                UserId = UserID, 
                DatePosted = DateTime.Now, 
                Title = request.Title, 
                Message = request.Message 
            };
            var response = await repository.AddPost(post);

            if (response.Success)
            {
                return Ok(post);
            }
            return UnprocessableEntity(response);
        }
        // Put
        [Authorize]
        [HttpPut]
        [Route("UpdatePost")]
        public async Task<IActionResult> UpdatePost(Post originalPost)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedPost = await repository.GetPost(originalPost.PostId);
            if (updatedPost == null)
            {
                return NotFound();
            }

            if (updatedPost.UserId != UserID)
            {
                return Unauthorized();
            }

            var response = await repository.UpdatePost(updatedPost, originalPost);
            if (response.Success)
            {
                return Ok(updatedPost);
            }
            return UnprocessableEntity(originalPost);
        }
        // Delete
        [Authorize]
        [HttpDelete]
        [Route("Delete/{postId}")]
        public async Task<IActionResult> DeletePost(int postId)
        {
            var postToDelete = await repository.GetPost(postId);
            if (postToDelete == null)
            {
                return NotFound();
            }

            if (postToDelete.UserId != UserID)
            {
                return Unauthorized();
            }

            var response = await repository.DeletePost(postToDelete);
            if (response.Success)
            {
                return Ok(response);
            }

            return UnprocessableEntity();
        }
    }
}
