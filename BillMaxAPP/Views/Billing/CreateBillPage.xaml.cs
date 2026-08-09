using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class CreateBillPage : ContentPage
{
    private readonly CreateBillViewModel _viewModel;

    public CreateBillPage(CreateBillViewModel viewModel)
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