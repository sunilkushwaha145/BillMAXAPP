namespace BillMaxAPP.Services;

public interface IPrinterService
{
    Task<bool> IsBluetoothEnabledAsync();

    Task<List<string>> GetPairedPrintersAsync();

    Task<bool> ConnectAsync(string printerName);

    Task<bool> PrintAsync(string content);

    Task DisconnectAsync();
}