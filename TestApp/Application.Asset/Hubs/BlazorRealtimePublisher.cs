using Application.Asset.Dtos;
using Application.Asset.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace Application.Asset.Hubs
{
    public class BlazorRealtimePublisher : IRealtimePublisher
    {
        private readonly IHubContext<AppHub> _hub;
        private readonly RealtimeEventBus _bus;

        public BlazorRealtimePublisher(
            IHubContext<AppHub> hub,
            RealtimeEventBus bus)
        {
            _hub = hub;
            _bus = bus;
        }

        public async Task PublishAsync<T>(string eventName, object? data = default)
        {
            var envelope = new RealtimeItem
            {
                Event = eventName,
                Time = DateTime.Now.ToString("HH:mm:ss"),
                Data = data,
                DataType = typeof(T).Name
            };

            // 🔥 1. Publish IN-PROCESS (Blazor Server)
            _bus.Publish(envelope);

            // 🔥 2. Publish qua SignalR (client khác)
            await _hub.Clients.All.SendAsync("OnRealtime", envelope);
        }

        public async Task PublishClientAsync(string BaseURL, string eventName, object data)
        {
            //throw new NotImplementedException();
        }
    }
}
