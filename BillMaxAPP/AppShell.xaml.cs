using BillMaxAPP.Services.Interfaces;
using BillMaxAPP.Views;

namespace BillMaxAPP
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
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
}