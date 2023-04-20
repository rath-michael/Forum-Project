using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels.Rooms
{
    public class AddRoomRequest
    {
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
