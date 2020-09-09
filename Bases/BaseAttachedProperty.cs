using System;
using System.Windows;

namespace Chat.Bases {
    /// <summary>
    /// Base attached property that replaces WPF attached property
    /// </summary>
    /// <typeparam name="Parent"> Parent class to be the attached property </typeparam>
    /// <typeparam name="Property"> Type of current property </typeparam>
    public abstract class BaseAttachedProperty<Parent, Property> where Parent : BaseAttachedProperty<Parent, Property>, new() {
        /// <summary>
        /// Singleton instance of parent class
        /// </summary>
        public static Parent Instance { get; } = new Parent();

        /// <summary>
        /// Attached property of this class
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new UIPropertyMetadata(OnValuePropertyChanged));

        /// <summary>
        /// Attached property's getter
        /// </summary>
        /// <param name="dependencyObject"> The element to get the property from </param>
        /// <returns> Actually attached property </returns>
        public static Property GetValue(DependencyObject dependencyObject) => (Property)dependencyObject.GetValue(ValueProperty);

        /// <summary>
        /// Attached property's setter
        /// </summary>
        /// <param name="dependencyObject"> The element to set the property from </param>
        /// <param name="value"> Value to set the property to </param>
        public static void SetValue(DependencyObject dependencyObject, Property value) => dependencyObject.SetValue(ValueProperty, value);

        /// <summary>
        /// Fires when value changes
        /// </summary>
        public event Action<DependencyObject, DependencyPropertyChangedEventArgs> ValueChanged = (sender, args) => {};

        /// <summary>
        /// Method, that is called every time when any attached property of this type is changed
        /// </summary>
        /// <param name="dependencyObject"> The UI element that this property was changed for </param>
        /// <param name="args"> Event's argument </param>
        public virtual void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) {}

        /// <summary>
        /// The callback event when <see cref="ValueProperty"/> is changed
        /// </summary>
        /// <param name="dependencyObject"> The UI element that had it's property changed </param>
        /// <param name="args"> Event's arguments </param>
        private static void OnValuePropertyChanged(DependencyObject                   dependencyObject,
                                                   DependencyPropertyChangedEventArgs args) {
            // Call the parent function
            Instance.OnValueChanged(dependencyObject, args);
            // Call event listeners
            Instance.ValueChanged(dependencyObject, args);
        }
    }
}