using System;
using System.Globalization;
using Xamarin.Forms;

namespace PoC_Thomas.Converters
{
    public class SexToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                Color color = Color.Default;
                switch (value)
                {
                    case "Male":
                        color = Color.MediumAquamarine;
                        break;
                    case "Female":
                        color = Color.MediumOrchid;
                        break;
                    default:
                        color = Color.NavajoWhite;
                        break;
                }
            return color.ToHex();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
