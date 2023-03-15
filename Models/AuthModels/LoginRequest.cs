using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.AuthModels
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}