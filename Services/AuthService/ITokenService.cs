using Forum_API_Provider.Models.AuthModels;

namespace Forum_API_Provider.Services.AuthService
{
    public interface ITokenService
    {
        Task<TokenResponse> GenerateTokensAsync(int userId);
        Task<bool> ValidateRefreshTokenAsync(UpdateTokensRequest request);
    }
}
