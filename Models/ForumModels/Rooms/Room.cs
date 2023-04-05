using Forum_API_Provider.Models.ForumModels.Posts;
using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Rooms
{
    public partial class Room
    {
        public int RoomId { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Description { get; set; }
        public int PostCount { get; set; }
        //public ICollection<Post> Posts { get; set; }
    }
}
