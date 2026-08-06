using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using BillMaxAPP.Views;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels;

public class LoginViewModel
{
    private readonly IAuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public ICommand LoginCommand { get; }

    public LoginViewModel(IAuthService authService, IServiceProvider serviceProvider    )
    {
        _authService = authService;
        LoginCommand = new Command(async () => await LoginAsync());
        _serviceProvider = serviceProvider;
    }

    private async Task LoginAsync()
    {
        try
        {
            var request = new LoginRequest
            {
                Username = Username,
                Password = Password
            };

            var result = await _authService.LoginAsync(request);

            if (result != null && result.Status.IsSuccess)
            {
                await SecureStorage.SetAsync("token", result.Data?.ToString());
                var appShell = _serviceProvider.GetRequiredService<AppShell>();
                Application.Current!.Windows[0].Page = appShell;
            }
            else
            {
                await Application.Current!.Windows[0].Page!
                    .DisplayAlert("Error",result.Status.Message, "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.Windows[0].Page!
                .DisplayAlert("Error", ex.Message, "OK");
        }
    }
}