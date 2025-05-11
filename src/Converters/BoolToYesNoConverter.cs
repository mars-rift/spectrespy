// src/BoolToYesNoConverter.cs
using System;
using System.Globalization;
using System.Windows.Data;

namespace spectrespy.Converters
{
    public class BoolToYesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Yes" : "No";
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue.ToLower() == "yes";
            }
            return false;
        }
    }
}
