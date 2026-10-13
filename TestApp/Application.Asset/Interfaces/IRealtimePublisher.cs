namespace Application.Asset.Interfaces
{
    public interface IRealtimePublisher
    {
        Task PublishAsync<T>(string eventName, object? data);
        Task PublishClientAsync(string BaseURL,string eventName, object data);
    }
}
