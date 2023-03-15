using Forum_API_Provider.Models.ForumModels;

namespace Forum_API_Provider.Services.ForumService
{
    public interface IForumRepository
    {
        // Room
        Task<List<Room>> GetAllRooms();
        Task<Room> GetRoom(int roomId);

        // Post
        Task<List<Post>> GetAllPosts();
        Task<Post> GetPost(int postId);
        Task<Post> AddPost(Post post);
        Task<Post> UpdatePost(Post post);
        void DeletePost(Post post);
    }
}
