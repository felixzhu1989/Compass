using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Compass.Wpf.Common.Converters;

public class ActivityToIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value!=null && Activity_e.TryParse(value.ToString(), out Activity_e result))
        {
            switch (result)
            {
                case Activity_e.Drawing:
                    return "AndroidStudio";
                case Activity_e.Nesting:
                    return "NewspaperVariant";
                case Activity_e.Cutting:
                    return "ScissorsCutting";
                case Activity_e.Bending:
                    return "TrayArrowDown";
                case Activity_e.Collect:
                    return "TrayFull";
                case Activity_e.Welding:
                    return "TrayAlert";
                case Activity_e.Polishing:
                    return "RoundedCorner";
                case Activity_e.Assembly:
                    return "ScrewMachineFlatTop";
                case Activity_e.Piping:
                    return "PipeLeak";
                case Activity_e.Ansul:
                    return "FireOff";
                case Activity_e.Electrical:
                    return "LightningBolt";
                case Activity_e.Quality:
                    return "AccountCheck";
                case Activity_e.Packing:
                    return "TruckCargoContainer";
            }
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
