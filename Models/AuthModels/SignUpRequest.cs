using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.AuthModels
{
    public class SignUpRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set;}
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}