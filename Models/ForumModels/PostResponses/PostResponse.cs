using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Users;
using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.PostResponses
{
    public class PostResponse
    {
        public int PostResponseId { get; set; }
        [Required]
        public int PostId { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public string Message { get; set; }
        //public Post Post { get; set; }
        //public User? User { get; set; }
    }
}
