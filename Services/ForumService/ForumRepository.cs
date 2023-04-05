using Forum_API_Provider.Models.ForumModels;
using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.Rooms;
using Forum_API_Provider.Models.ForumModels.Users;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Forum_API_Provider.Services.ForumService
{
    public class ForumRepository : IForumRepository
    {
        private ForumDbContext context;
        public ForumRepository(ForumDbContext context)
        {
            this.context = context;
        }

        #region Room
        public async Task<List<Room>> GetAllRooms()
        {
            var rooms = await context.Rooms.ToListAsync();
            foreach (var room in rooms)
            {
                room.PostCount = await context.Posts
                    .Where(p => p.RoomId == room.RoomId)
                    .CountAsync();
            }
            return rooms;
        }
        public async Task<Room> GetRoom(int roomId)
        {
            return await context.Rooms
                .Where(r => r.RoomId == roomId)
                .SingleOrDefaultAsync();
        }
        #endregion


        #region Post
        public async Task<Post> GetPost(int postId)
        {
            return await context.Posts
                .Where(p => p.PostId == postId)
                .SingleOrDefaultAsync();
        }
        public async Task<List<Post>> GetAllPosts()
        {
            return await context.Posts.ToListAsync();
        }
        public async Task<PostsByRoomResponse> GetPostsByRoom(int roomId)
        {
            var room = await context.Rooms.Where(r => r.RoomId == roomId).SingleOrDefaultAsync();
            if (room != null)
            {
                var posts = await context.Posts.Where(p => p.RoomId == roomId).ToListAsync();
                var response = new PostsByRoomResponse
                {
                    Success = true,
                    Message = "",
                    Room = room,
                    Posts = posts
                };
                return response;
            }
            return new PostsByRoomResponse { Success = false, Message = "Invalid room" };
            
        }
        public async Task<PostsByUserResponse> GetPostsByUser(int userId)
        {
            return null;
        }
        public async Task<AddPostResponse> AddPost(Post post)
        {
            await context.Posts.AddAsync(post);
            var response = await context.SaveChangesAsync();

            if (response >= 0)
            {
                return new AddPostResponse
                {
                    Success = true,
                    Message = "Post successfully added",
                    Post = post
                };
            }

            return new AddPostResponse 
            { 
                Success = false,
                Message = "Post add failure"
            };
        }
        public async Task<UpdatePostResponse> UpdatePost(Post post, int userId)
        {
            var updatedPost = await context.Posts.FindAsync(post.PostId);
            if (updatedPost == null)
            {
                return new UpdatePostResponse
                {
                    Success = false,
                    Message = "Post not found"
                };
            }

            if (updatedPost.UserId != userId)
            {
                return new UpdatePostResponse
                {
                    Success = false,
                    Message = "You do not have access to update this post"
                };
            }

            updatedPost.Title = post.Title;
            updatedPost.Message = post.Message;
            var response = await context.SaveChangesAsync();

            if (response >= 0)
            {
                return new UpdatePostResponse
                {
                    Success = true,
                    Message = "Post successfully updated",
                    Post = updatedPost
                };
            }

            return new UpdatePostResponse
            {
                Success = false,
                Message = "Unable to update post"
            };
        }
        public async Task<DeletePostResponse> DeletePost(int postId, int userId)
        {
            var post = await context.Posts.FindAsync(postId);
            if (post == null)
            {
                return new DeletePostResponse
                {
                    Success = false,
                    Message = "Post not found"
                };
            }

            if (post.UserId == userId)
            {
                return new DeletePostResponse
                {
                    Success = false,
                    Message = "You don't have access to delete this post"
                };
            }

            context.Posts.Remove(post);
            var response = await context.SaveChangesAsync();

            if (response >= 0)
            {
                return new DeletePostResponse
                {
                    Success = true,
                    Message = "Post successfully deleted"
                };
            }

            return new DeletePostResponse
            {
                Success = false,
                Message = "Unable to delete post"
            };
        }
        #endregion
    }
}
