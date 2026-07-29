using Microsoft.AspNetCore.Mvc;
using Uber.Application.DTOs.Ride;
using Uber.Application.Interfaces;

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

        [HttpPost("book")]
        public async Task<IActionResult> BookRide([FromBody] BookRideDto dto)
        {
            var result = await _rideService.BookRideAsync(dto);

            return Ok(result);
        }
    }
}