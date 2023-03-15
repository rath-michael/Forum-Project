using Forum_API_Provider.Models.AuthModels;
using Forum_API_Provider.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Collections;

namespace Forum_API_Provider.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        private ITokenService tokenService;
        public UserController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> SignUp(SignUpRequest request)
        {
            // Verify model state and return any errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(m => m.ErrorMessage).ToList());
                if (errors.Any())
                {
                    return BadRequest(new
                    {
                        Errors = string.Join(", ", errors)
                    });
                }
            }

            // Attempt to register new user, return result
            var signupAttempt = await userService.SignUpAsync(request);
            if (!signupAttempt.Success)
            {
                return UnprocessableEntity(signupAttempt);
            }
            return Ok(request.Email);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            // Verify model state and return any errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(e => e.Errors.Select(m => m.ErrorMessage).ToList());
                if (errors.Any())
                {
                    return BadRequest(new
                    {
                        Errors = string.Join(", ", errors)
                    });
                }
            }

            // Attempt to log user in, return result
            var loginAttempt = await userService.LoginAsync(request);
            if (!loginAttempt.Success)
            {
                return UnprocessableEntity(loginAttempt);
            }
            return Ok(loginAttempt);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout(int userId)
        {
            // Attempt to log user out, return result
            var logoutAttempt = await userService.LogoutAsync(userId);
            if (!logoutAttempt.Success)
            {
                return UnprocessableEntity(logoutAttempt);
            }
            return Ok(logoutAttempt);
        }

        [HttpPost]
        [Route("UpdateTokens")]
        public async Task<IActionResult> UpdateTokens(UpdateTokensRequest request)
        {
            // Verify request contents
            if (request == null || string.IsNullOrEmpty(request.RefreshToken) || request.UserId == 0)
            {
                return BadRequest(new { Success = false, Message = "Invalid request details"});
            }

            // Verify refresh token validity
            var validToken = await tokenService.ValidateRefreshTokenAsync(request);
            if (!validToken)
            {
                return UnprocessableEntity(new { Success = false, Message = "Invalid refresh token"});
            }

            // Generate new tokens
            var tokens = await tokenService.GenerateTokensAsync(request.UserId);

            return Ok(new { AccessToken = tokens.Token, RefreshToken = tokens.RefreshToken });
        }
    }
}
