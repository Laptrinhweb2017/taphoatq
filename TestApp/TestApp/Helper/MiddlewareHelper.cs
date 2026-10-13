using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ServiceLib.Services;
using ModelLib.Repositories;
using Application.Asset.Dtos;
using Application.Asset.Interfaces;
using ViewModel;
using Application.Asset.ViewDtos;

namespace TestApp.Helper
{
    public class MiddlewareHelper
    {
        private void AddRepoService<TDto, TRepo, TService>(WebApplicationBuilder builder)
            where TRepo : class, IRepository<TDto>
            where TService : class, IService<TDto>
        {
            builder.Services.AddScoped<IRepository<TDto>, TRepo>();
            builder.Services.AddScoped<IService<TDto>, TService>();
            builder.Services.AddScoped<TService>();
        }
        public void RegisterServices(WebApplicationBuilder builder)
        {
           //----Repo Service----
			AddRepoService<DtoLoginInfo, LoginInfoRepo, LoginInfoService>(builder);
			AddRepoService<DtoCustomer, CustomerRepo, CustomerService>(builder);
			AddRepoService<DtoEmployee, EmployeeRepo, EmployeeService>(builder);
			AddRepoService<DtoSupplier, SupplierRepo, SupplierService>(builder);
			AddRepoService<DtoStoreInfo, StoreInfoRepo, StoreInfoService>(builder);
			AddRepoService<DtoBrand, BrandRepo, BrandService>(builder);
			AddRepoService<DtoCategory, CategoryRepo, CategoryService>(builder);
			AddRepoService<DtoProducts, ProductsRepo, ProductsService>(builder);
			AddRepoService<DtoPrice, PriceRepo, PriceService>(builder);
			AddRepoService<DtoWarehouses, WarehousesRepo, WarehousesService>(builder);
			AddRepoService<DtoInventory, InventoryRepo, InventoryService>(builder);
			AddRepoService<DtoStockInput, StockInputRepo, StockInputService>(builder);
			AddRepoService<DtoInputDetail, InputDetailRepo, InputDetailService>(builder);
			AddRepoService<DtoOrderStatus, OrderStatusRepo, OrderStatusService>(builder);
			AddRepoService<DtoOrders, OrdersRepo, OrdersService>(builder);
			AddRepoService<DtoOrderDetail, OrderDetailRepo, OrderDetailService>(builder);
			AddRepoService<DtoStockOutput, StockOutputRepo, StockOutputService>(builder);
			AddRepoService<DtoOutputDetail, OutputDetailRepo, OutputDetailService>(builder);
			AddRepoService<DtoTrackingStatus, TrackingStatusRepo, TrackingStatusService>(builder);
			AddRepoService<DtoPOTracking, POTrackingRepo, POTrackingService>(builder);
			AddRepoService<DtoPaymentMethod, PaymentMethodRepo, PaymentMethodService>(builder);
			AddRepoService<DtoPayment, PaymentRepo, PaymentService>(builder);
			AddRepoService<DtoPaymentDetail, PaymentDetailRepo, PaymentDetailService>(builder);

            builder.Services.AddScoped<LoginRepo>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddScoped<HeaderInfo>();

            //----Tools----


            //----ViewModel----

            var services = builder.Services;
			services.Scan(scan => scan
					.FromAssemblyOf<ViewModelMarker>()
					.AddClasses(classes => classes.Where(x=>x.Name.EndsWith("ViewModel")))
					.AsSelf()
					.WithScopedLifetime()
				);

        }
        public void RegisterAuthen(WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();
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
            builder.Services.AddBlazorBootstrap();
        }

    }
}
