using BillMaxAPP.Models;
using BillMaxAPP.Services.Interfaces;
using BillMaxAPP.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels;

public class InvoiceListViewModel : INotifyPropertyChanged
{
    private readonly IBillService _billService;

    private List<Invoices> _allInvoices = new();


    private Invoices? _selectedInvoice;

    public Invoices? SelectedInvoice
    {
        get => _selectedInvoice;
        set
        {
            if (_selectedInvoice == value)
                return;

            _selectedInvoice = value;
            OnPropertyChanged();
        }
    }

    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (_isBusy == value)
                return;

            _isBusy = value;
            OnPropertyChanged();
        }
    }

    private string? _searchText;
    public string? SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value)
                return;

            _searchText = value;
            OnPropertyChanged();

            ApplySearch();
        }
    }

    public ObservableCollection<RecentBillDto> Invoices { get; } = new();

    public ICommand ViewCommand { get; }


    public InvoiceListViewModel(IBillService billService)
    {
        _billService = billService;

        ViewCommand = new Command<RecentBillDto>(
           async invoice => await ViewInvoiceAsync(invoice));
    }

    public async Task InitializeAsync()
    {
        if (Invoices.Count == 0)
            await LoadInvoicesAsync();
    }

    public async Task LoadInvoicesAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var result = await _billService.GetBillHistoryAsync();

            if (result == null || !result.Status.IsSuccess)
            {
                _allInvoices.Clear();
                Invoices.Clear();
                return;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var response =
                ((JsonElement)result.Data)
                .Deserialize<List<Invoices>>(options);

            _allInvoices = response ?? new List<Invoices>();

            ApplySearch();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplySearch()
    {
        IEnumerable<Invoices> result = _allInvoices;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var search = SearchText.Trim();

            result = _allInvoices.Where(x =>
                x.InvoiceId.ToString().Contains(search) ||
                (x.Customers?.Name?.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ?? false) ||
                (x.Customers?.Mobile?.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase) ?? false));
        }

        Invoices.Clear();
        foreach (var invoice in result)
        {
            RecentBillDto recentBillDto = new RecentBillDto
            {
                InvoiceId = invoice.InvoiceId,
                CustomerName = invoice.Customers?.Name ?? "Unknown",
                InvoiceNo=invoice.InvoiceId.ToString(),
                ItemsCount= invoice.InvoiceItems?.Count ?? 0,
                GrandTotal=invoice.GrandTotal,
                PayType= invoice.PayType,
                PayStatus= invoice.PayStatus,
                CreatedDate=invoice.Crd,
            };  
            Invoices.Add(recentBillDto);
        }
    }

    public async Task RefreshAsync()
    {
        await LoadInvoicesAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }

    private async Task ViewInvoiceAsync(RecentBillDto? invoice)
    {
        if (invoice == null)
            return;

        var selectedInvoice = _allInvoices
            .FirstOrDefault(x => x.InvoiceId == invoice.InvoiceId);

        if (selectedInvoice == null)
            return;

        SelectedInvoice = selectedInvoice;

        await Shell.Current.Navigation.PushAsync(
            new InvoiceDetailsPage(selectedInvoice));
    }
}