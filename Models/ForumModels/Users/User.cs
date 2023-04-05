using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Forum_API_Provider.Models.ForumModels;
using Forum_API_Provider.Models.ForumModels.PostResponses;
using Forum_API_Provider.Models.ForumModels.Posts;

namespace Forum_API_Provider.Models.ForumModels.Users
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
        //public ICollection<Post> Posts { get; set; }
        //public ICollection<PostResponse> PostResponses { get; set; }
    }
}
