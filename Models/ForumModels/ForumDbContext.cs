using Microsoft.EntityFrameworkCore;

namespace Forum_API_Provider.Models.ForumModels
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Email = "mike@mail.com",
                    UserName = "MRath",
                    Password = "KMLunUSx3gqyO3LdhJr/H6aRnKLXN7mf24Z/QHVuyYk=",
                    PasswordSalt = "8OfcirkrYBrwkCJa9BTogihr7XTVhLqqPcQ6PnUdKX0=",
                    FirstName = "mike",
                    LastName = "rath"
                },
                new User
                {
                    UserId = 2,
                    Email = "josh@mail.com",
                    UserName = "JMarks",
                    Password = "KMLunUSx3gqyO3LdhJr/H6aRnKLXN7mf24Z/QHVuyYk=",
                    PasswordSalt = "8OfcirkrYBrwkCJa9BTogihr7XTVhLqqPcQ6PnUdKX0=",
                    FirstName = "josh",
                    LastName = "marks"
                });

            modelBuilder.Entity<Room>().HasData(
                new Room { RoomId = 1, RoomName = "Room One" },
                new Room { RoomId = 2, RoomName = "Room Two" },
                new Room { RoomId = 3, RoomName = "Room Three" }
                );

            modelBuilder.Entity<Post>().HasData(
                new Post { PostId = 1, UserId = 1, Title = "Test Title One" },
                new Post { PostId = 2, UserId = 1, Title = "Test Title Two" },
                new Post { PostId = 3, UserId = 1, Title = "Test Title Three" },
                new Post { PostId = 4, UserId = 1, Title = "Test Title Four" }
                );
        }
    }
}
