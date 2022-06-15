using Compass.Shared.Const;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Compass.Wpf.Common.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && ProjectStatus_e.TryParse(value.ToString(), out ProjectStatus_e result))
            {
                switch (result)
                {
                    case ProjectStatus_e.CsDrwMaking:
                        return "LightSkyBlue";
                    case ProjectStatus_e.CsDrwChecking:
                        return "SkyBlue";
                    case ProjectStatus_e.CsDrwApproved:
                        return "DeepSkyBlue";
                    case ProjectStatus_e.GettingOdp:
                        return "DarkKhaki";
                    case ProjectStatus_e.KickOff:
                        return "Khaki";
                    case ProjectStatus_e.DrwMaking:
                        return "Yellow";
                    case ProjectStatus_e.DrwReleased:
                        return "Gold";
                    case ProjectStatus_e.InProduction:
                        return "Orange";
                    case ProjectStatus_e.ProdCompleted:
                        return "SpringGreen";
                    case ProjectStatus_e.ProjCompleted:
                        return "Green";
                    case ProjectStatus_e.Cancel:
                        return "Gray";
                    case ProjectStatus_e.Suspend:
                        return "Pink";
                    case ProjectStatus_e.Abnormal:
                        return "Red";
                }
            }            
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
