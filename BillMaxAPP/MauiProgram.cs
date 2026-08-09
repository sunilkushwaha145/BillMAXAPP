using BillMaxAPP;
using BillMaxAPP.Helpers;
using BillMaxAPP.Services;
using BillMaxAPP.Services.Interfaces;
using BillMaxAPP.ViewModels;
using BillMaxAPP.Views;
using LiveChartsCore.SkiaSharpView.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()          // ✅ ADD THIS
            .UseLiveCharts()         // LiveCharts handler registration
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(ApiRoutes.BaseUrl)
        });

        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddSingleton<IAuthService, AuthService>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddSingleton<StoreMgntPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddSingleton<GSTSettingPage>();
        builder.Services.AddSingleton<ReportPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddSingleton<IDashboardService, DashboardService>();
        builder.Services.AddTransient<AdminDashboardPage>();
        builder.Services.AddTransient<AdminDashboardViewModel>();
        builder.Services.AddTransient<SplashPage>();
        builder.Services.AddTransient<AppShellStore>();
        builder.Services.AddTransient<StoreDashboardViewModel>();

        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IProductService, ProductService>();
        builder.Services.AddSingleton<IBillService, BillService>();
        builder.Services.AddTransient<CreateBillViewModel>();
        builder.Services.AddTransient<CreateBillPage>();
        return builder.Build();
    }
}