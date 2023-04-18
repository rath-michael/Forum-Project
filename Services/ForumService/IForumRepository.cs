using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Rooms;
using Forum_API_Provider.Models.ForumModels.Users;

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
        Task<PostsByRoomResponse> GetPostsByRoom(Room room);
        Task<PostsByUserResponse> GetPostsByUser(User user);
        Task<AddPostResponse> AddPost(Post post);
        Task<UpdatePostResponse> UpdatePost(Post updatedPost, Post originalPost);
        Task<DeletePostResponse> DeletePost(Post post);
        #endregion

        #region User
        Task<User> GetUser(int userId);
        #endregion
    }
}
