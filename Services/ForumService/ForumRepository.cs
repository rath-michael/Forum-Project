using Forum_API_Provider.Models.DbModels;
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
        public async Task<List<Room>> GetAllRooms()
        {
            return await context.Rooms.ToListAsync();
        }
        public async Task<List<Post>> GetAllPosts()
        {
            return await context.Posts.ToListAsync();
        }
    }
}
