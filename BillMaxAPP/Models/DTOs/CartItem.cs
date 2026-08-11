using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BillMaxAPP.Models;

public class CartItem : INotifyPropertyChanged
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ProductImage { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal GSTPercentage { get; set; }

    private int _qty = 1;

    public int Qty
    {
        get => _qty;
        set
        {
            if (_qty == value)
                return;

            _qty = value;

            OnPropertyChanged();
            OnPropertyChanged(nameof(ItemTotal));
        }
    }

    public decimal ItemTotal =>
        UnitPrice * Qty;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}