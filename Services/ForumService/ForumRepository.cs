using Forum_API_Provider.Models.DbModels;

namespace Forum_API_Provider.Services.ForumService
{
    public class ForumRepository : IForumRepository
    {
        private ForumDbContext context;
        public ForumRepository(ForumDbContext context)
        {
            this.context = context;
        }
        public List<Room> GetAllRooms()
        {
            return context.Rooms.ToList();
        }
        public List<Post> GetAllPosts()
        {
            return context.Posts.ToList();
        }
    }
}
