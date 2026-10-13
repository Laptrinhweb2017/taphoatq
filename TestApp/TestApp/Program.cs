using TestApp.Helper;
namespace TestApp
{
	public class Program

	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);


			// ===== CONFIG + SERVER =====

			builder.AddAppConfiguration();
			builder.AddAppSettings();

			builder.AddAppServer();


			// ===== FRAMEWORK =====

			builder.AddBlazor();

			builder.AddSignalRApp();


			// ===== Model DI =====

			MiddlewareHelper middleware = new();

			middleware.RegisterAuthen(builder);

			middleware.RegisterServices(builder);



			var app = builder.Build();


			// ===== PIPELINE =====

			app.UseAppPipeline();


			app.Run();

		}

	}

}

