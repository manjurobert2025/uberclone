using Microsoft.AspNetCore.SignalR;
using Uber.API.Hubs;
using Uber.Application.Interfaces;

namespace Uber.API
{
    public class RideNotificationService : IRideNotificationService
    {
        private readonly IHubContext<RideHub> _hubContext;

        public RideNotificationService(
            IHubContext<RideHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyDriverNewRideAsync(
            string driverId,
            Guid rideId)
        {
            await _hubContext.Clients
                .Group($"driver-{driverId}")
                .SendAsync(
                    "NewRideRequest",
                    new
                    {
                        RideId = rideId
                    });
        }
    }
}