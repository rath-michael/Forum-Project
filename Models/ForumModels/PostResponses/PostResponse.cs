using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Users;

namespace Forum_API_Provider.Models.ForumModels.PostResponses
{
    public class PostResponse
    {
        public int PostResponseId { get; set; }
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
    }
}
