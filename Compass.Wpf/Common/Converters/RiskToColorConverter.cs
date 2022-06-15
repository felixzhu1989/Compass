using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Compass.Wpf.Common.Converters;
public class RiskToColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value!=null && int.TryParse(value.ToString(), out int result))
        {
            switch (result)
            {
                case 1:
                    return "Red";
                case 2:
                    return "Pink";
                case 3:
                    return "LightSkyBlue";
            }
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
