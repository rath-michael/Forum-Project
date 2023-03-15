namespace Forum_API_Provider.Models.AuthModels
{
    public class TokenResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
