using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Application.Asset.Hubs
{
    [AllowAnonymous] // ← dùng auth cookie hiện tại
    public class AppHub : Hub
    {
        private readonly RealtimeEventBus _bus;

        public AppHub(RealtimeEventBus bus)
        {
            _bus = bus;
        }

        public Task OnRealtime(RealtimeItem Item)
        {
            _bus.Publish(Item);
            return Task.CompletedTask;
        }
    }
}
