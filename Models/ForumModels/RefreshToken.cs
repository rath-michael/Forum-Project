namespace Forum_API_Provider.Models.ForumModels
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TokenHash { get; set; }
        public string TokenSalt { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
