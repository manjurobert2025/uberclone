using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Uber.Application.DTOs.Ride;
using Uber.Application.Interfaces;
using System.Security.Claims;
namespace Uber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideController : ControllerBase
    {
        private readonly IRideService _rideService;

        public RideController(IRideService rideService)
        {
            _rideService = rideService;
        }

        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> BookRide([FromBody] BookRideDto dto)
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            var riderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(riderId))
            {
                return Unauthorized("User ID not found.");
            }

            dto.RiderId = Guid.Parse(riderId);

            var result = await _rideService.BookRideAsync(dto);

            return Ok(result);
        }
    }
}