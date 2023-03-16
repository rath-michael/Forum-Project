using Forum_API_Provider.Models.AuthModels;
using Forum_API_Provider.Models.ForumModels;
using Forum_API_Provider.Models.ForumModels.Users;
using Forum_API_Provider.Services.ForumService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Forum_API_Provider.Services.AuthService
{
    public class UserService : IUserService
    {
        private ForumDbContext context;
        private ITokenService tokenService;
        public UserService(ForumDbContext context, ITokenService tokenService)
        {
            this.context = context;
            this.tokenService = tokenService;
        }

        public async Task<SignUpResponse> SignUpAsync(SignUpRequest request)
        {
            // Check if user with same email or username already exists
            var userExists = context.Users.Any(u => u.Email == request.Email || u.UserName == request.Username);
            if (userExists)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "User with that email or username already exists"
                };
            }

            // Verify passwords match
            if (request.Password != request.ConfirmPassword)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Passwords must match"
                };
            }

            // Verify password length
            if (request.Password.Length <= 4)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Message = "Password must be at least 5 characters"
                };
            }

            // Get password hash and salt for new user db entry
            var passwordSalt = PasswordHelper.GetSecureSalt();
            var hashedPassword = PasswordHelper.GetPasswordHash(request.Password, passwordSalt);

            // Create new user
            var newUser = new User()
            {
                Email = request.Email,
                UserName = request.Username,
                Password = hashedPassword,
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            // Save new user to db
            await context.Users.AddAsync(newUser);
            var saveResponse = context.SaveChanges();

            // If save to db successful, return success
            if (saveResponse > 0)
            {
                return new SignUpResponse
                {
                    Success = true,
                    Message = "New user created"
                };
            }
            // If save to db fail, return fail
            return new SignUpResponse
            {
                Success = false,
                Message = "Unable to save user"
            };
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // Check if user exists
            var user = context.Users.SingleOrDefault(u => u.Email == request.Email);
            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            // Check if correct password
            var password = PasswordHelper.GetPasswordHash(request.Password, Convert.FromBase64String(user.PasswordSalt));
            if (user.Password != password)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid password"
                };
            }

            // Get tokens
            var tokenAttempt = await tokenService.GenerateTokensAsync(user.UserId);

            // Return result
            if (tokenAttempt != null && tokenAttempt.Success)
            {
                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Token = tokenAttempt.Token,
                    RefreshToken = tokenAttempt.RefreshToken
                };
            }
            return new LoginResponse
            {
                Success = false,
                Message = "Login unsuccessful",
            };
        }

        public async Task<LogoutResponse> LogoutAsync(int userId)
        {
            // Get refresh token associated with supplied userId from db
            var refreshToken = await context.RefreshTokens.FirstOrDefaultAsync(o => o.UserId == userId);
            
            // If no current token found, return true
            if (refreshToken == null)
            {
                return new LogoutResponse
                {
                    Success = true
                };
            }

            // Remove current refresh token from db
            context.RefreshTokens.Remove(refreshToken);

            // Save change to db
            var saveResponse = await context.SaveChangesAsync();

            // If save successful, return true
            if (saveResponse >= 0)
            {
                return new LogoutResponse { Success = true };
            }

            // Else return false
            return new LogoutResponse
            {
                Success = false,
                Message = "Logout failed"
            };
        }
    }
}
