using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class InvoiceListPage : ContentPage
{
    private readonly InvoiceListViewModel _viewModel;

    public InvoiceListPage(InvoiceListViewModel viewModel)
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
}