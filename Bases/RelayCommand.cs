namespace Chat {
    
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base implementation of ICommand interface
    /// </summary>
    public class RelayCommand : ICommand {

        /// <summary>
        /// Constructor of RelayCommand
        /// </summary>
        /// <param name="action"> Action that must be executed if it possible </param>
        /// <param name="canExecute"> Func that checks if we can execute action (if null, action is always executable) </param>
        public RelayCommand(Action action, Func<object, bool> canExecute = null) {
            this._canExecute = canExecute;
            this._action = action;
        }

        public bool CanExecute(object parameter) => this._canExecute?.Invoke(parameter) ?? true;

        public void Execute(object parameter) => this._action?.Invoke();

        public event EventHandler CanExecuteChanged = (sender, args) => {};

        protected Func<object, bool> _canExecute;
        protected Action _action;
    }
}