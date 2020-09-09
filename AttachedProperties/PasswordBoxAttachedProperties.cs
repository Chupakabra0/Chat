using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;
using Chat.Bases;

namespace Chat.AttachedProperties {
    /// <summary>
    /// Attached property for <see cref="PasswordBox"/> that indicates if it needs to monitor changes in it
    /// </summary>
    public class MonitorPasswordProperty : BaseAttachedProperty<MonitorPasswordProperty, bool> {
        public override void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {
            if (!(dependencyObject is PasswordBox passwordBox)) return;

            passwordBox.PasswordChanged -= PasswordBox_PasswordChanged;

            if (!(args.NewValue is bool)) return;

            HasTextProperty.SetValue(passwordBox);
            passwordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        /// <summary>
        /// <see cref="PasswordBox"/> password change event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            HasTextProperty.SetValue((PasswordBox)sender);
        }
    }

    /// <summary>
    /// Attached property for <see cref="PasswordBox"/> that indicates if it has any text
    /// </summary>
    public class HasTextProperty : BaseAttachedProperty<HasTextProperty, bool> {
        /// <summary>
        /// Sets <see cref="HasTextProperty"/> based on if caller <see cref="PasswordBox"/> has any text
        /// </summary>
        /// <param name="dependencyObject">  </param>
        public static void SetValue(DependencyObject dependencyObject) {
            HasTextProperty.SetValue(dependencyObject, ((PasswordBox)dependencyObject).SecurePassword.Length > 0);
        }
    }
}
