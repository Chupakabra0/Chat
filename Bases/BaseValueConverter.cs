using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Chat.Bases {
    /// <summary>
    /// Base value converter that allow directly XAML usage
    /// </summary>
    /// <typeparam name="T"> The type of this value converter </typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new() {
        /// <summary>
        /// Converts value to targetType
        /// </summary>
        /// <param name="value"> Value that needs to be converted </param>
        /// <param name="targetType"> The type the value should become </param>
        /// <param name="parameter"> Additional parameter </param>
        /// <param name="culture"> Culture info </param>
        /// <returns> Converted value </returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts value back to it's source type
        /// </summary>
        /// <param name="value"> Value that needs to be converted </param>
        /// <param name="targetType"> The type the value should become </param>
        /// <param name="parameter"> Additional parameter </param>
        /// <param name="culture"> Culture info </param>
        /// <returns> Converted value back </returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Calls when XAML tries directly get value
        /// </summary>
        /// <param name="serviceProvider"> Service that provides value </param>
        /// <returns> Single instance of this value converter </returns>
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return _converter ??= new T();
        }

        /// <summary>
        /// Single static instance of this value converter
        /// </summary>
        protected static T _converter;
    }
}
