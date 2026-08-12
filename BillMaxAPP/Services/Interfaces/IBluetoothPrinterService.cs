namespace BillMaxAPP.Services;

public interface IBluetoothPrinterService
{
    bool IsBluetoothEnabled();

    Task<List<Models.BluetoothPrinter>> GetPairedPrintersAsync();

    Task<bool> ConnectAsync(string printerName);

    Task<bool> TestPrintAsync();

    Task<bool> PrintAsync(string content);

    Task<bool> PrintBytesAsync(byte[] data);

    bool IsConnected();

    Task DisconnectAsync();
}