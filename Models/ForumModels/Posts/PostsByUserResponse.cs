using Forum_API_Provider.Models.ForumModels.Users;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class PostsByUserResponse : APIResponse
    {
        public User User { get; set; }
        public List<Post> Posts { get; set; }
    }
}
