using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private readonly IAuthService _authService;
    private readonly IServiceProvider _serviceProvider;

    private string _username = string.Empty;
    private string _password = string.Empty;
    private bool _rememberMe = true;
    private bool _isBusy;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public bool RememberMe
    {
        get => _rememberMe;
        set { _rememberMe = value; OnPropertyChanged(); }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotBusy)); }
    }

    public bool IsNotBusy => !IsBusy;

    public ICommand LoginCommand { get; }

    public LoginViewModel(IAuthService authService, IServiceProvider serviceProvider)
    {
        _authService = authService;
        _serviceProvider = serviceProvider;
        LoginCommand = new Command(async () => await LoginAsync(), () => !IsBusy);
    }

    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            await Application.Current!.Windows[0].Page!
                .DisplayAlert("Missing Information", "Please enter both email/mobile and password.", "OK");
            return;
        }

        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var request = new LoginRequest
            {
                Username = Username,
                Password = Password
            };

            var result = await _authService.LoginAsync(request);

            if (result != null && result.Status.IsSuccess)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var response = ((JsonElement)result.Data).Deserialize<LoginData>(options);
                var token = response?.Token;

                if (RememberMe)
                {
                    await SecureStorage.SetAsync("token",response.Token);
                    await SecureStorage.SetAsync("roleId", response.User.RoleId.ToString());
                    await SecureStorage.SetAsync("userId", response.User.UserId.ToString());
                    await SecureStorage.SetAsync("userName", response.User.UserName);
                }
                else
                {
                    // Still store for current session, but you could
                    // use a separate in-memory/session-only store here
                    await SecureStorage.SetAsync("token", response.Token);
                    await SecureStorage.SetAsync("roleId", response.User.RoleId.ToString());
                    await SecureStorage.SetAsync("userId", response.User.UserId.ToString());
                    await SecureStorage.SetAsync("userName", response.User.UserName);
                }

                if(response.User.RoleId == 1)
                {
                    var appShell = _serviceProvider.GetRequiredService<AppShell>();
                    Application.Current!.Windows[0].Page = appShell;
                }
                else if (response.User.RoleId == 2)
                {
                    var appShell = _serviceProvider.GetRequiredService<AppShellStore>();
                    Application.Current!.Windows[0].Page = appShell;
                }
                else
                {
                    await Application.Current!.Windows[0].Page!
                        .DisplayAlert("Error", "Unknown role. Cannot navigate.", "OK");
                }

            }
            else
            {
                await Application.Current!.Windows[0].Page!
                    .DisplayAlert("Error", result?.Status?.Message ?? "Login failed", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current!.Windows[0].Page!
                .DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}