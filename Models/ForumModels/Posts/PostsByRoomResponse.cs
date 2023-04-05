using Forum_API_Provider.Models.ForumModels.Rooms;

namespace Forum_API_Provider.Models.ForumModels.Posts
{
    public class PostsByRoomResponse : APIResponse
    {
        public Room Room { get; set; }
        public List<Post> Posts { get; set; }
    }
}
