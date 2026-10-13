public static class AppServerExtensions
{
    public static WebApplicationBuilder AddAppServer(this WebApplicationBuilder builder)
    {
        builder.WebHost.ConfigureKestrel(options =>
        {
            options.Limits.MaxRequestBodySize = 500_000_000;
        });

        return builder;
    }
}
