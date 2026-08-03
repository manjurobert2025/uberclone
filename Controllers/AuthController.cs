using Microsoft.AspNetCore.Mvc;
using Uber.Application.DTOs;
using Uber.Application.DTOs.Auth;
using Uber.Application.DTOs.User;
using Uber.Application.Interfaces;

namespace Uber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            return Ok(new
            {
                message = result
            });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            return Ok(result);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var result = await _authService.ForgotPasswordAsync(dto);

            // Keep the response generic for security
            return Ok("If the email is registered, a password reset link will be sent.");
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _authService.ResetPasswordAsync(dto);

            if (!result)
            {
                return BadRequest("Invalid or expired reset token.");
            }

            return Ok("Password reset successfully.");
        }
    }
}