using BillMaxAPP.Models;
using BillMaxAPP.ViewModels;

namespace BillMaxAPP.Views;

public partial class InvoiceDetailsPage : ContentPage
{
    public InvoiceDetailsPage(Invoices invoice)
    {
        InitializeComponent();

        BindingContext = invoice;
    }

    private async void BackButton_Clicked(
       object sender,
       EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
