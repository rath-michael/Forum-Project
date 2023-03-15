using Forum_API_Provider.Models.DbModels;

namespace Forum_API_Provider.Services.ForumService
{
    public interface IForumRepository
    {
        Task<List<Room>> GetAllRooms();
        Task<List<Post>> GetAllPosts();
    }
}
