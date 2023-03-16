using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class AddPostResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Post Post { get; set; }
    }
}
