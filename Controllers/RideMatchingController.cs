using Microsoft.AspNetCore.Mvc;
using Uber.Application.DTOs.RideMatching;
using Uber.Application.Interfaces;

namespace Uber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RideMatchingController : ControllerBase
    {
        private readonly IRideMatchingService _rideMatchingService;

        public RideMatchingController(IRideMatchingService rideMatchingService)
        {
            _rideMatchingService = rideMatchingService;
        }

        [HttpPost("match")]
        public async Task<IActionResult> MatchDriver([FromBody] MatchDriverDto dto)
        {
            var result = await _rideMatchingService.MatchDriverAsync(dto);

            return Ok(result);
        }
    }
}