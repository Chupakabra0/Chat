namespace Chat {
    
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;
    using PropertyChanged;
    
    /// <summary>
    /// Base view-model with implementation of INotifyPropertyChanged interface
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class BasicVM : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Inform view, that some property have changed
        /// </summary>
        /// <param name="propertyName"> Caller property name </param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}