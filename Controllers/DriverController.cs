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
    }
}