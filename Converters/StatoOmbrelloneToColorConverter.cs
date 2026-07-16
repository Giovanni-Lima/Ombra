using System.Globalization;
using Ombra.Models;

namespace Ombra.Converters;

public class StatoOmbrelloneToColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var key = value switch
        {
            StatoOmbrellone.Libero => "StatoLibero",
            StatoOmbrellone.Prenotato => "StatoPrenotato",
            StatoOmbrellone.Occupato => "StatoOccupato",
            _ => "Gray300"
        };
        return Application.Current?.Resources[key] ?? Colors.Gray;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
