using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BillMaxAPP.Models;

public class CategoryOption : INotifyPropertyChanged
{
    private bool _disabled;
    private bool _selected;
    private string? _text;
    private string? _value;
    private string? _iconUrl;

    public bool Disabled
    {
        get => _disabled;
        set
        {
            if (_disabled == value)
                return;

            _disabled = value;
            OnPropertyChanged();
        }
    }

    public bool Selected
    {
        get => _selected;
        set
        {
            if (_selected == value)
                return;

            _selected = value;
            OnPropertyChanged();
        }
    }

    public string? Text
    {
        get => _text;
        set
        {
            if (_text == value)
                return;

            _text = value;
            OnPropertyChanged();
        }
    }

    public string? Value
    {
        get => _value;
        set
        {
            if (_value == value)
                return;

            _value = value;
            OnPropertyChanged();
        }
    }

    public string? IconUrl
    {
        get => _iconUrl;
        set
        {
            if (_iconUrl == value)
                return;

            _iconUrl = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(
            this,
            new PropertyChangedEventArgs(propertyName));
    }
}