public static class SignalRExtensions
{
    public static WebApplicationBuilder AddSignalRApp(this WebApplicationBuilder builder)
    {
        builder.Services.AddSignalR(options =>
        {
            options.MaximumReceiveMessageSize = 1024 * 1024 * 500;
        });
        return builder;
    }
}
