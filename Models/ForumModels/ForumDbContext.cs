using Forum_API_Provider.Models.ForumModels.Posts;
using Forum_API_Provider.Models.ForumModels.PostResponses;
using Forum_API_Provider.Models.ForumModels.Rooms;
using Forum_API_Provider.Models.ForumModels.Users;
using Microsoft.EntityFrameworkCore;
using Azure;

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
        public virtual DbSet<PostResponse> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(r => r.Room)
                    .WithMany(p => p.Posts)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(u => u.User)
                    .WithMany(p => p.Posts)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<PostResponse>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostResponses)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostResponses)
                    .OnDelete(DeleteBehavior.SetNull);
            });

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
                new Room { RoomId = 1, RoomName = "Room One", Description = "Room One Description" },
                new Room { RoomId = 2, RoomName = "Room Two", Description = "Room Two Description" },
                new Room { RoomId = 3, RoomName = "Room Three", Description = "Room Three Description" }
                );

            modelBuilder.Entity<Post>().HasData(
                new Post { PostId = 1, RoomId = 1, UserId = 1, DatePosted = DateTime.Now, Title = "Test Title One", Message = "Test Message One" },
                new Post { PostId = 2, RoomId = 2, UserId = 1, DatePosted = DateTime.Now, Title = "Test Title Two", Message = "Test Message Two" },
                new Post { PostId = 3, RoomId = 3, UserId = 1, DatePosted = DateTime.Now, Title = "Test Title Three", Message = "Test Message Three" },
                new Post { PostId = 4, RoomId = 1, UserId = 1, DatePosted = DateTime.Now, Title = "Test Title Four", Message = "Test Message Four" }
                );
        }
    }
}
