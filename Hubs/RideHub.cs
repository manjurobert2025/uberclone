using Microsoft.AspNetCore.SignalR;

namespace Uber.API.Hubs
{
    public class RideHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public async Task JoinDriver(string driverId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"driver-{driverId}"
            );

            Console.WriteLine(
                $"Driver {driverId} joined SignalR group. Connection: {Context.ConnectionId}"
            );
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
}