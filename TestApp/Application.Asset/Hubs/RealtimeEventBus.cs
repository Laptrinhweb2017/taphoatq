namespace Application.Asset.Hubs
{
    public class RealtimeEventBus
    {
        public event Action<RealtimeItem>? OnMessage;

        public void Publish(RealtimeItem Item)
        {
            OnMessage?.Invoke(Item);
        }
    }

}
