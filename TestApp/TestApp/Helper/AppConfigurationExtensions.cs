using Application.Asset.Dtos;

public static class AppConfigurationExtensions
{
    public static WebApplicationBuilder AddAppConfiguration(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile(
                $"appsettings.{builder.Environment.EnvironmentName}.json",
                true);
        return builder;
    }
    public static WebApplicationBuilder AddAppSettings(
    this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<AppSettings>()
            .Bind(builder.Configuration.GetSection("AppSettings"))
            .Validate(s => !string.IsNullOrWhiteSpace(s.CurrentHost),
                "AppSettings:CurrentHost is required")
            .ValidateOnStart();

        return builder;
    }
}
