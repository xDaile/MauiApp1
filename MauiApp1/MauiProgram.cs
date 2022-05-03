using MauiApp1.Shells;
using MauiApp1.Views;
using MauiApp1.ViewModels;
using MauiApp1.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
		ConfigureShell(builder.Services);
		ConfigureViews(builder.Services);
		ConfigureViewModels(builder.Services);
		ConfigureServices(builder.Services);


		var app = builder.Build();
		RegisterRoutes(app);
		return app;
	}

    private static void ConfigureShell(IServiceCollection services)
    {
        services.AddSingleton<AppShellPhone>();
        /*
        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
        {
            services.AddSingleton<AppShellPhone>();
        }
        else
        {
            services.AddSingleton<AppShellDesktop>();
        }
        */
    }
    
    private static void ConfigureViews(IServiceCollection services)
    {
        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<ContentPageBase>())
            .AsSelf()
            .WithTransientLifetime());
    }

    private static void ConfigureViewModels(IServiceCollection services)
    {
        services.Scan(selector => selector
            .FromAssemblyOf<App>()
            .AddClasses(filter => filter.AssignableTo<IViewModel>())
            .AsSelfWithInterfaces()
            .WithTransientLifetime());
    }
    
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRoutingService, RoutingService>();
        //services.AddSingleton<IDeviceOrientationService, DeviceOrientationService>();
        //services.AddSingleton<IShare>(_ => Share.Default);
    }

    private static void RegisterRoutes(MauiApp app)
    {
        var routingService = app.Services.GetRequiredService<IRoutingService>();

        foreach (var routeModel in routingService.Routes)
        {
            Routing.RegisterRoute(routeModel.Route, routeModel.ViewType);
        }
    }
}
