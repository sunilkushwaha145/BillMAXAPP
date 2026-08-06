using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class AdminDashboardPage : ContentPage
{
    private readonly AdminDashboardViewModel _viewModel;
    private bool _isLoaded;


    public AdminDashboardPage(AdminDashboardViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // API sirf first time call hogi
        if (_isLoaded)
            return;

        _isLoaded = true;

        await _viewModel.LoadDashboardAsync();
    }
}