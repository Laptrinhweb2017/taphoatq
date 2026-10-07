using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asset.Interfaces
{
    public interface IRealtimePublisher
    {
        Task PublishAsync<T>(string eventName, object? data);
        Task PublishClientAsync(string BaseURL,string eventName, object data);
    }
}
