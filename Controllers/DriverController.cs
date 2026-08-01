using Microsoft.AspNetCore.Mvc;
using Uber.Application.DTOs.Driver;
using Uber.Application.Interfaces;

namespace Uber.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _driverService;

        public DriverController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterDriver(RegisterDriverDto dto)
        {
            var result = await _driverService.RegisterDriverAsync(dto);

            return Ok(result);
        }
        [HttpPut("{driverId}/status")]
        public async Task<IActionResult> UpdateStatus(
         Guid driverId,
         DriverStatusDto dto)
        {
            var result = await _driverService.UpdateDriverStatus(driverId, dto.IsOnline);

            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpPut("{driverId}/location")]
        public async Task<IActionResult> UpdateLocation(
    Guid driverId,
    UpdateLocationDto dto)
        {
            var result = await _driverService.UpdateLocationAsync(driverId, dto);

            if (!result)
                return NotFound();

            return Ok(new
            {
                message = "Location updated successfully"
            });
        }
    }
}