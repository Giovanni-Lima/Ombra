using System.Globalization;
using Ombra.Models;

namespace Ombra.Converters;

// Lo sfondo "StatoPrenotato" (Ocra Sole) è troppo chiaro per il testo Sabbia usato sugli altri due stati:
// serve testo scuro (Notte Marina) solo in quel caso per mantenere un contrasto leggibile.
public class StatoOmbrelloneToTextColorConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var key = value is StatoOmbrellone.Prenotato ? "NotteMarina" : "Sabbia";
        return Application.Current?.Resources[key] ?? Colors.Black;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
