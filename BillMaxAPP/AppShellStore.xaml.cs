using BillMaxAPP.Services.Interfaces;
using BillMaxAPP.Views;

namespace BillMaxAPP;

public partial class AppShellStore : Shell
{
    public AppShellStore()
    {
        InitializeComponent();

        // Create Bill is opened on demand (via the "+" button), not shown as
        // a bottom tab — register it as a navigable route instead.
        Routing.RegisterRoute("createbill", typeof(CreateBillPage));
    }
    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "Cancel");
        if (!confirm) return;

        var authService = IPlatformApplication.Current!.Services.GetRequiredService<IAuthService>();
        await authService.LogoutAsync();

        var loginPage = IPlatformApplication.Current!.Services.GetRequiredService<LoginPage>();
        Application.Current!.Windows[0].Page = new NavigationPage(loginPage);
    }
}