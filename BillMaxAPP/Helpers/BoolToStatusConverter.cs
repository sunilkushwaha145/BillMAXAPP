using System.Globalization;

namespace BillMaxAPP.Helpers;

public class BoolToStatusConverter : IValueConverter
{
    public object Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        if (value is bool status)
            return status ? "Paid" : "Unpaid";

        return "Unpaid";
    }

    public object ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}