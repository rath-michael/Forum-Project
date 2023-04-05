using Forum_API_Provider.Models.ForumModels.PostResponses;
using Forum_API_Provider.Models.ForumModels.Rooms;
using Forum_API_Provider.Models.ForumModels.Users;
using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime DatePosted { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Message { get; set; }
        //public Room Room { get; set; }
        //public User User { get; set; }
        //public ICollection<PostResponse> PostResponses { get; set; }
    }
}
