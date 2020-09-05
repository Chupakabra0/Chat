using System.Windows;

namespace Chat {
    using System.Windows.Controls;

    public static class PasswordBoxProperties {

        public static readonly DependencyProperty HasTextProperty = DependencyProperty.RegisterAttached("HasText", typeof(bool), typeof(PasswordBoxProperties),
                                                                                                        new PropertyMetadata(false));

        public static bool GetHasText(PasswordBox passwordBox) =>
            (bool)passwordBox.GetValue(HasTextProperty);

        private static void SetHasText(PasswordBox passwordBox) =>
            passwordBox.SetValue(HasTextProperty, passwordBox.SecurePassword.Length > 0);
        
        public static readonly DependencyProperty MonitorPasswordProperty = DependencyProperty.RegisterAttached("MonitorPassword", typeof(bool), typeof(PasswordBoxProperties),
                                                                                                                  new PropertyMetadata(false, MonitorPasswordChanged));

        public static bool GetMonitorPassword(PasswordBox passwordBox) =>
            (bool)passwordBox.GetValue(MonitorPasswordProperty);

        public static void SetMonitorPassword(PasswordBox passwordBox, bool value) =>
            passwordBox.SetValue(MonitorPasswordProperty, value);

        public static void MonitorPasswordChanged(DependencyObject                   obj,
                                                  DependencyPropertyChangedEventArgs args) {

            if (!(obj is PasswordBox passwordBox)) return;

            passwordBox.PasswordChanged -= PasswordBoxOnPasswordChanged;

            if (!(args.NewValue is bool)) return;

            SetHasText(passwordBox);
            passwordBox.PasswordChanged += PasswordBoxOnPasswordChanged;
        }

        private static void PasswordBoxOnPasswordChanged(object sender, RoutedEventArgs e) {
            SetHasText((PasswordBox)sender);
        }
    }
}
