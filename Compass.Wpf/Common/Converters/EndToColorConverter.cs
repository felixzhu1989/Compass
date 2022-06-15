using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Compass.Wpf.Common.Converters;

public class EndToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value!=null && DateTime.TryParse(value.ToString(), out DateTime result))
        {
            if ((result-DateTime.Now).Days<0) return "LightGreen";
        }
        return "Pink";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
