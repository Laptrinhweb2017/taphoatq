using Business;
using DataLayer.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Services;
using ViewModal;

namespace BachHoaWeb.Helper
{
    public class MiddleWareHelper
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<CategoryRepo>(); //khai báo thêm dịch vụ dependency injection
            builder.Services.AddScoped<CategoryService>();

            builder.Services.AddScoped<CustomerRepo>();
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<CustomerManager>();
            builder.Services.AddScoped<CustomerViewModel>();

            builder.Services.AddScoped<EmployeeRepo>();
            builder.Services.AddScoped<EmployeeService>();
            builder.Services.AddScoped<EmployeeManager>();
            builder.Services.AddScoped<EmployeeViewModel>();



            builder.Services.AddScoped<LoginInfoRepo>();
            builder.Services.AddScoped<LoginInfoService>();
            builder.Services.AddScoped<LoginViewModel>();


            builder.Services.AddScoped<LayoutService>();



        }
        public void RegisterAuthen(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddHttpClient();
           // builder.Services.AddScoped<IFileStorageService, FileStorageService>();
            builder.Services.AddControllersWithViews(); //1st
            builder.Services.AddRazorPages();//2nd
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/login";
                    options.Cookie.Name = "AuthCookie";
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.Cookie.HttpOnly = true;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                });

            builder.Services.AddAntiforgery();
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthenStateProvider>();
            //builder.Services.AddBlazorBootstrap();
        }
    }
}
