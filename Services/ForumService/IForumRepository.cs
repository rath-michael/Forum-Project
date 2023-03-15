using Forum_API_Provider.Models.DbModels;

namespace Forum_API_Provider.Services.ForumService
{
    public interface IForumRepository
    {
        List<Room> GetAllRooms();
        List<Post> GetAllPosts();
    }
}
