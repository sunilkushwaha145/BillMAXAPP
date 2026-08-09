using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class StoreDashboardPage : ContentPage
{

    private readonly StoreDashboardViewModel _viewModel;
    private bool _isLoaded;


    public StoreDashboardPage(StoreDashboardViewModel viewModel)
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

    private async void OnNewBillClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("createbill");
    }
}