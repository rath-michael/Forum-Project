using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class AddPostRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
