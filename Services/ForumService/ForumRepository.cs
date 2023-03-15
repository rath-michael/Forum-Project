using Forum_API_Provider.Models.ForumModels;
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
        // Room
        public async Task<List<Room>> GetAllRooms()
        {
            return await context.Rooms.ToListAsync();
        }
        public async Task<Room> GetRoom(int roomId)
        {
            return await context.Rooms.FindAsync(roomId);
        }

        // Post
        public async Task<List<Post>> GetAllPosts()
        {
            return await context.Posts.ToListAsync();
        }
        public async Task<Post> GetPost(int postId)
        {
            return await context.Posts.FindAsync(postId);
        }
        public async Task<Post> AddPost(Post post)
        {
            var result = await context.Posts.AddAsync(post);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Post> UpdatePost(Post post)
        {
            var postToUpdate = await context.Posts.FindAsync(post.PostId);
            if (postToUpdate != null)
            {
                postToUpdate.PostId = post.PostId;
                postToUpdate.UserId = post.UserId;
                postToUpdate.Title = post.Title;

                await context.SaveChangesAsync();
                return postToUpdate;
            }

            return null;
        }
        public async void DeletePost(Post post)
        {
            context.Posts.Remove(post);
            await context.SaveChangesAsync();
        }
    }
}
