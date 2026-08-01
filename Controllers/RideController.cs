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
        [HttpGet("pending/{driverId}")]
        public async Task<IActionResult> GetPendingRide(Guid driverId)
        {
            var ride = await _rideService.GetPendingRideAsync(driverId);

            if (ride == null)
                return NoContent();

            return Ok(ride);
        }
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptRide(Guid rideId, Guid driverId)
        {
            var accepted = await _rideService.AcceptRideAsync(rideId, driverId);

            if (!accepted)
                return BadRequest("Ride cannot be accepted.");

            return Ok("Ride accepted successfully.");
        }
        [HttpPost("{rideId}/start")]
        public async Task<IActionResult> StartRide(Guid rideId)
        {
            var started = await _rideService.StartRideAsync(rideId);

            if (!started)
                return BadRequest("Ride cannot be started.");

            return Ok("Ride started successfully.");
        }
        [HttpPost("{rideId}/complete")]
        public async Task<IActionResult> CompleteRide(Guid rideId)
        {
            var completed = await _rideService.CompleteRideAsync(rideId);

            if (!completed)
                return BadRequest("Ride cannot be completed.");

            return Ok("Ride completed successfully.");
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