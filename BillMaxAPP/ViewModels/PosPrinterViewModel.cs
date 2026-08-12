using BillMaxAPP.Models;
using BillMaxAPP.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace BillMaxAPP.ViewModels;

public class PosPrinterViewModel : INotifyPropertyChanged
{
    private readonly IBluetoothPrinterService _printerService;

    public ObservableCollection<BluetoothPrinter> Printers { get; } = new();

    private string _connectedPrinterName = "No printer connected";

    public string ConnectedPrinterName
    {
        get => _connectedPrinterName;
        set
        {
            if (_connectedPrinterName == value)
                return;

            _connectedPrinterName = value;
            OnPropertyChanged();
        }
    }

    private string _connectedPrinterAddress = string.Empty;

    public string ConnectedPrinterAddress
    {
        get => _connectedPrinterAddress;
        set
        {
            if (_connectedPrinterAddress == value)
                return;

            _connectedPrinterAddress = value;
            OnPropertyChanged();
        }
    }

    private string _connectionStatus = "Disconnected";

    public string ConnectionStatus
    {
        get => _connectionStatus;
        set
        {
            if (_connectionStatus == value)
                return;

            _connectionStatus = value;
            OnPropertyChanged();
        }
    }

    public ICommand ConnectCommand { get; }

    public ICommand TestPrintCommand { get; }

    public ICommand DisconnectCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public PosPrinterViewModel(
        IBluetoothPrinterService printerService)
    {
        _printerService = printerService;

        ConnectCommand = new Command<BluetoothPrinter>(
            async printer => await ConnectAsync(printer));

        TestPrintCommand = new Command(
            async () => await TestPrintAsync());

        DisconnectCommand = new Command(
            async () => await DisconnectAsync());
    }

    public async Task InitializeAsync()
    {
        await LoadPrintersAsync();
    }

    public async Task LoadPrintersAsync()
    {
        try
        {
            Printers.Clear();

            var printers =
                await _printerService.GetPairedPrintersAsync();

            foreach (var printer in printers)
                Printers.Add(printer);

            if (printers.Count == 0)
            {
                await ShowAlertAsync(
                    "Bluetooth",
                    "No paired Bluetooth devices found. Please pair your thermal printer from Android Bluetooth settings.");
            }
        }
        catch (Exception ex)
        {
            await ShowAlertAsync(
                "Bluetooth",
                ex.Message);
        }
    }

    private async Task ConnectAsync(
        BluetoothPrinter? printer)
    {
        if (printer == null)
            return;

        try
        {
            ConnectionStatus = "Connecting...";

            await _printerService.ConnectAsync(printer.Name);

            ConnectedPrinterName = printer.Name;
            ConnectedPrinterAddress = printer.Address;
            ConnectionStatus = "Connected";
        }
        catch (Exception ex)
        {
            ConnectionStatus = "Disconnected";

            await ShowAlertAsync(
                "Printer Connection",
                ex.Message);
        }
    }

    private async Task TestPrintAsync()
    {
        try
        {
            if (!_printerService.IsConnected())
            {
                await ShowAlertAsync(
                    "Printer",
                    "Please connect a printer first.");

                return;
            }

            await _printerService.TestPrintAsync();

            await ShowAlertAsync(
                "Test Print",
                "Test receipt sent successfully.");
        }
        catch (Exception ex)
        {
            await ShowAlertAsync(
                "Print Error",
                ex.Message);
        }
    }

    private async Task DisconnectAsync()
    {
        await _printerService.DisconnectAsync();

        ConnectedPrinterName = "No printer connected";
        ConnectedPrinterAddress = string.Empty;
        ConnectionStatus = "Disconnected";
    }

    private static async Task ShowAlertAsync(
        string title,
        string message)
    {
        if (Application.Current?.Windows.Count > 0)
        {
            var page =
                Application.Current.Windows[0].Page;

            if (page != null)
            {
                await page.DisplayAlert(
                    title,
                    message,
                    "OK");
            }
        }
    }

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}