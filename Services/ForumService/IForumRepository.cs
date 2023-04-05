using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Rooms;

namespace Forum_API_Provider.Services.ForumService
{
    public interface IForumRepository
    {
        #region Room
        Task<List<Room>> GetAllRooms();
        Task<Room> GetRoom(int roomId);
        #endregion

        #region Post
        Task<Post> GetPost(int postId);
        Task<List<Post>> GetAllPosts();
        Task<PostsByRoomResponse> GetPostsByRoom(int roomId);
        Task<PostsByUserResponse> GetPostsByUser(int userId);
        Task<AddPostResponse> AddPost(Post post);
        Task<UpdatePostResponse> UpdatePost(Post post, int userId);
        Task<DeletePostResponse> DeletePost(int postId, int userId);
        #endregion
    }
}
