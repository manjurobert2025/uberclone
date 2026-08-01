using Microsoft.AspNetCore.Mvc;
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
    }
}