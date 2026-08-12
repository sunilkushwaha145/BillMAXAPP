#if ANDROID

using Android.Bluetooth;
using BillMaxAPP.Models;
using BillMaxAPP.Services;
using System.Text;

namespace BillMaxAPP.Platforms.Android.Services;

public class BluetoothPrinterService : IBluetoothPrinterService
{
    private BluetoothAdapter? _adapter;
    private BluetoothSocket? _socket;

    public BluetoothPrinterService()
    {
        _adapter = BluetoothAdapter.DefaultAdapter;
    }

    public bool IsBluetoothEnabled()
    {
        return _adapter?.IsEnabled == true;
    }

    public Task<List<BluetoothPrinter>> GetPairedPrintersAsync()
    {
        var printers = new List<BluetoothPrinter>();

        if (_adapter == null)
            return Task.FromResult(printers);

        foreach (var device in _adapter.BondedDevices)
        {
            if (!string.IsNullOrWhiteSpace(device.Name))
            {
                printers.Add(new BluetoothPrinter
                {
                    Name = device.Name,
                    Address = device.Address
                });
            }
        }

        return Task.FromResult(printers);
    }

    public async Task<bool> ConnectAsync(string printerName)
    {
        try
        {
            if (_adapter == null || !_adapter.IsEnabled)
                return false;

            var device = _adapter.BondedDevices?
                .FirstOrDefault(x => x.Name == printerName);

            if (device == null)
                return false;

            await DisconnectAsync();

            var uuid = Java.Util.UUID.FromString(
                "00001101-0000-1000-8000-00805F9B34FB");

            _socket = device.CreateRfcommSocketToServiceRecord(uuid);

            await _socket.ConnectAsync();

            return _socket.IsConnected;
        }
        catch
        {
            await DisconnectAsync();
            return false;
        }
    }

    public async Task<bool> TestPrintAsync()
    {
        const string testText =
            "\x1B\x40" +
            "       BILLMAX\r\n" +
            "    PRINTER TEST\r\n" +
            "------------------------------\r\n" +
            "Bluetooth Printer Connected\r\n" +
            "------------------------------\r\n\r\n\r\n";

        return await PrintAsync(testText);
    }

    public async Task<bool> PrintAsync(string content)
    {
        try
        {
            if (_socket == null || !_socket.IsConnected)
                return false;

            var data = Encoding.ASCII.GetBytes(content);

            await _socket.OutputStream.WriteAsync(
                data,
                0,
                data.Length);

            await _socket.OutputStream.FlushAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> PrintBytesAsync(byte[] data)
    {
        try
        {
            if (_socket == null || !_socket.IsConnected)
                return false;

            await _socket.OutputStream.WriteAsync(
                data,
                0,
                data.Length);

            await _socket.OutputStream.FlushAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool IsConnected()
    {
        return _socket?.IsConnected == true;
    }

    public Task DisconnectAsync()
    {
        try
        {
            _socket?.Close();
            _socket?.Dispose();
        }
        catch
        {
        }

        _socket = null;

        return Task.CompletedTask;
    }
}

#endif