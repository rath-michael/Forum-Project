using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Rooms;

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
        Task<AddPostResponse> AddPost(Post post);
        Task<UpdatePostResponse> UpdatePost(Post post, int userId);
        Task<DeletePostResponse> DeletePost(int postId, int userId);
    }
}
