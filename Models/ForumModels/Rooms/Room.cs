using Forum_API_Provider.Models.ForumModels.Posts;

namespace Forum_API_Provider.Models.ForumModels.Rooms
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
