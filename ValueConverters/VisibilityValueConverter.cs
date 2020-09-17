using System;
using System.Globalization;
using System.Windows;
using Chat.Bases;

namespace Chat.ValueConverters {
    public class VisibilityValueConverter : BaseValueConverter<VisibilityValueConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (!(value is bool boolean)) throw new Exception("Not boolean.");

            return boolean ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}