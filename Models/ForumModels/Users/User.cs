using System.ComponentModel.DataAnnotations;
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
        public string Password { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<PostResponse>? PostResponses { get; set; }
    }
}
