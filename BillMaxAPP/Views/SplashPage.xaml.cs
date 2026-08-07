using Microsoft.Extensions.DependencyInjection;

namespace BillMaxAPP.Views;

public partial class SplashPage : ContentPage
{
    public SplashPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(2200);

        var token = await SecureStorage.GetAsync("token");

        if (string.IsNullOrWhiteSpace(token))
        {
            var loginPage = IPlatformApplication.Current!.Services.GetRequiredService<LoginPage>();
            Application.Current!.Windows[0].Page = new NavigationPage(loginPage);
        }
        else
        {
            var appShell = IPlatformApplication.Current!.Services.GetRequiredService<AppShell>();
            Application.Current!.Windows[0].Page = appShell;
        }
    }
}