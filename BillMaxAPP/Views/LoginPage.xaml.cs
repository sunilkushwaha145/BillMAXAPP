using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class LoginPage : ContentPage
{
    private bool _showPassword = false;
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnBackToHomeTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Info", "Back to Home clicked", "OK");

        // Ya navigation:
        // await Navigation.PopAsync();
    }

    private async void OnForgotPasswordTapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Forgot Password", "Coming Soon", "OK");
    }

    private void OnTogglePasswordClicked(object sender, EventArgs e)
    {
        _showPassword = !_showPassword;

        PasswordEntry.IsPassword = !_showPassword;

        // Agar do icons hain:
        // TogglePasswordButton.Source = _showPassword ? "eye_off.png" : "eye.png";
    }
}