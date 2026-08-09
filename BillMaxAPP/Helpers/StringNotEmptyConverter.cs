using System.Globalization;

namespace BillMaxAPP.Helpers
{
    // Returns true if the bound string is non-null/non-whitespace.
    // Used to toggle between a real product/category image and a fallback icon.
    public class StringNotEmptyConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value is string s && !string.IsNullOrWhiteSpace(s);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}