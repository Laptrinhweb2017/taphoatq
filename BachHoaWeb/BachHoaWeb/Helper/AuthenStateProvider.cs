using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

public class AuthenStateProvider : ServerAuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenStateProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Luôn luôn bốc trực tiếp User từ HttpContext tại thời điểm gọi 
        // thay vì gán chết một lần trong Constructor
        var httpContext = _httpContextAccessor.HttpContext;
        var user = httpContext?.User ?? new ClaimsPrincipal(new ClaimsIdentity());

        return Task.FromResult(new AuthenticationState(user));
    }
}