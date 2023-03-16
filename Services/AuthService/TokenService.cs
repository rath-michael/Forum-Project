using Forum_API_Provider.Models.AuthModels;
using Forum_API_Provider.Models.ForumModels;
using Forum_API_Provider.Models.ForumModels.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Forum_API_Provider.Services.AuthService
{
    public class TokenService : ITokenService
    {
        private readonly ForumDbContext context;
        private IConfiguration configuration;
        public TokenService(ForumDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public async Task<TokenResponse> GenerateTokensAsync(int userId)
        {
            // Generate access and refresh tokens
            var accToken = await GenerateAccessToken(userId);
            var refToken = await GenerateRefreshToken();

            // Get user db record associated with supplied userId
            var userRecord = await context.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync();
            
            // If no record found, return empty
            if (userRecord == null) return null;

            // Get hash and salt for refresh token db entry
            var salt = PasswordHelper.GetSecureSalt();
            var refTokenHash = PasswordHelper.GetPasswordHash(refToken, salt);

            // Remove old refresh token
            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);
            }

            // Add new refresh token to db
            userRecord.RefreshTokens?.Add(new RefreshToken
            {
                UserId = userId,
                TokenHash = refTokenHash,
                TokenSalt = Convert.ToBase64String(salt),
                ExpireDate = DateTime.UtcNow.AddDays(30)
            });

            // Save db
            await context.SaveChangesAsync();

            // Return tokens
            return new TokenResponse
            {
                Success = true,
                Token = accToken,
                RefreshToken = refToken
            };
        }

        private async Task<string> GenerateAccessToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenKey = Encoding.ASCII.GetBytes(configuration.GetSection("JWT")["Secret"]);
            var tokenIssuer = configuration.GetSection("JWT")["Issuer"];
            var tokenAudience = configuration.GetSection("JWT")["Audience"];
            var tokenSubject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                });
            var tokenCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = tokenIssuer,
                Audience = tokenAudience,
                Subject = tokenSubject,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = tokenCredentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.Run(() => tokenHandler.WriteToken(token));
        }

        private async Task<string> GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            await Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));
            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;
        }

        private async Task RemoveRefreshTokenAsync(User user)
        {
            // Get user record with associated refresh token db entry
            var userRecord = await context.Users.Include(o => o.RefreshTokens)
                .FirstOrDefaultAsync(e => e.UserId == user.UserId);

            // If no record found, return
            if (userRecord == null) return;

            // If refresh token in db associated with user isn't null
            // and has an entry, remove it and return
            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                var currentRefreshToken = user.RefreshTokens.First();
                context.RefreshTokens.Remove(currentRefreshToken);
                return;
            }

            // Else, return
            return;
        }

        public async Task<bool> ValidateRefreshTokenAsync(UpdateTokensRequest request)
        {
            // Get token associated with supplied userId
            var refreshToken = await context.RefreshTokens.FirstAsync(o => o.UserId == request.UserId);

            // If null, return invalid
            if (refreshToken == null) return false;

            // Get hashed refresh token from db
            var validateHash = PasswordHelper.GetPasswordHash(
                request.RefreshToken,
                Convert.FromBase64String(refreshToken.TokenSalt
                ));

            // Verify hashed db token against user supplied refresh token
            // If no match, return invalid
            if (refreshToken.TokenHash != validateHash) return false;
            
            // Check if refresh token expired
            if (refreshToken.ExpireDate < DateTime.UtcNow) return false;

            // Else, return valid
            return true;
        }
    }
}
