using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class PosPrinterPage : ContentPage
{
    private readonly PosPrinterViewModel _viewModel;

    public PosPrinterPage(PosPrinterViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.InitializeAsync();
    }

    private async void BackButton_Clicked(
        object sender,
        EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void RefreshButton_Clicked(
        object sender,
        EventArgs e)
    {
        await _viewModel.LoadPrintersAsync();
    }
}