using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Restaurant.Api.Hubs
{
    public class RestaurantHub : Hub
    {
        [Authorize]
        public async Task JoinMeseroGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Meseros");
        }
        public async Task JoinCocineroGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Cocineros");
        }
        public async Task LeaveMeseroGrouo()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Meseros");
        }
        public async Task LeaveCocineroGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Cocineros");
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Meseros");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Cocineros");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
