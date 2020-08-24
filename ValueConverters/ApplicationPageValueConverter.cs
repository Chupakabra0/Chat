using System;
using System.Globalization;
using Chat.Bases;
using Chat.DataModels;
using Chat.Pages;

namespace Chat.ValueConverters {
    /// <summary>
    /// Converts <see cref="ApplicationPage"/> to an actual view/page
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter> {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (ApplicationPage)value switch {
                    ApplicationPage.Login => new LoginPage(),
                    _ => null
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
