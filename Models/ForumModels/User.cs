using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.ForumModels
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
    }
}
