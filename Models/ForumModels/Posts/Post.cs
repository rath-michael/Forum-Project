using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
