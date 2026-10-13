public static class BlazorExtensions
{
    public static WebApplicationBuilder AddBlazor(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        return builder;
    }
}
