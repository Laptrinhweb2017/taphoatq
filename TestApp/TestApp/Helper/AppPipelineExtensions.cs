using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using TestApp.Components;
using ViewModel;
public static class AppPipelineExtensions
{
    public static WebApplication UseAppPipeline(this WebApplication app)
    {
        // ===== Localization =====
        var supportedCultures = new[] { "vi-VN", "en-US" };

        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("vi-VN")
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        localizationOptions.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
        app.UseRequestLocalization(localizationOptions);

        // ===== Middlewares =====
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseWebSockets();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAntiforgery();

        app.MapControllers();
        
        // ===== Endpoint đổi ngôn ngữ =====
        app.MapGet("/culture/set", (HttpContext context, string culture, string? redirectUri) =>
        {
            var requestCulture = new RequestCulture(culture);

            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(requestCulture),
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1),
                    Path = "/",
                    IsEssential = true,
                    SameSite = SameSiteMode.Lax,
                    Secure = context.Request.IsHttps
                });

            return Results.Redirect(string.IsNullOrWhiteSpace(redirectUri) ? "/" : redirectUri);
        });
        app.MapGet("/logout", async (HttpContext httpContext, LoginViewModel vm) =>
        {
            if (httpContext.User.Identity?.IsAuthenticated == true)
            {
                await httpContext.SignOutAsync();
                vm.Logout();
            }
            return Results.Redirect("/");
        });

        // ===== Blazor endpoints (PHẢI đặt sau culture/set) =====
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        return app;
    }
}
