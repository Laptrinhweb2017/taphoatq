namespace Application.Asset.Hubs
{
    public class RealtimeItem
    {
        public string Event { get; set; } = "";          // Add / Update / Delete / Notify / Ping
        public string Time { get; set; } = "";           // HH:mm:ss
        public object? Data { get; set; }                // có thể null
        public string? DataType { get; set; } = "";
    }
}
