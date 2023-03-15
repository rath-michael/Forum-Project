using System.ComponentModel.DataAnnotations;

namespace Forum_API_Provider.Models.AuthModels
{
    public class UpdateTokensRequest
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
