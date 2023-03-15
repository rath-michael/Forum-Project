using Forum_API_Provider.Models.AuthModels;

namespace Forum_API_Provider.Services.AuthService
{
    public interface IUserService
    {
        Task<SignUpResponse> SignUpAsync(SignUpRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<LogoutResponse> LogoutAsync(int userId);
    }
}
