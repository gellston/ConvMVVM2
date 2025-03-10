using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ConvMVVM2.WPF.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var e = (Enum)value;
            var str = e.ToString();
            return Regex.Replace(str, @"([a-z])([A-Z])", "$1 $2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            var str = (string)value;
            str = str.Replace(" ", "");
            return Enum.Parse(targetType, str, true);
        }
    }
}
