using System.Windows;
using System.Windows.Input;
using Chat.DataModels;
using Fasetto.Word;
using System.Windows.Controls;

namespace Chat {
    /// <summary>
    /// Window view model
    /// </summary>
    public class WindowVM : BasicVM {
        /// <summary>
        /// Constructor of view model
        /// </summary>
        /// <param name="window"> Standard window </param>
        public WindowVM(Window window) {
            // Window and dock initialization
            this._window = window;
            this._dockPosition = WindowDockPosition.Undocked;

            this._window.StateChanged += (sender, args) => {
                this.WindowResized();
            };

            this.Title = "Welcome home!";
            this.CurrentPage = ApplicationPage.Login;
            this.OuterMargin = 10d;
            this.WindowRadius = 10d;
            this.TitleHeight = 40d;
            this.ContentPadding = 0d;
            this.MinHeight = 400d;
            this.MinWidth = 400d;

            // Fixes the issue with Windows of Style WindowStyle.None covering the taskbar
            var windowResizer = new WindowResizer(this._window);

            // Watches by window dock changes
            windowResizer.WindowDockChanged += dock => {
                // Remembers las tposition
                this._dockPosition = dock;

                // Fire off resize events
                this.WindowResized();
            };
        }

        /// <summary>
        /// Command to minimize window
        /// </summary>
        public  ICommand MinimizeCommand => this._minimizeCommand ??= new RelayCommand(() => { 
            this._window.WindowState = WindowState.Minimized;
        });
        private ICommand _minimizeCommand;

        /// <summary>
        /// Command to maximize window
        /// </summary>
        public  ICommand MaximizeCommand => this._maximizeCommand ??= new RelayCommand(() => {
            // ReSharper disable once BitwiseOperatorOnEnumWithoutFlags
            // If window is already maximized, it becomes 0,
            // that actually means, that window state becomes Normal 
            this._window.WindowState ^= WindowState.Maximized;
        });
        private ICommand _maximizeCommand;


        /// <summary>
        /// Command to close window
        /// </summary>
        public  ICommand CloseCommand => this._closeCommand ??= new RelayCommand(() => {
            this._window.Close();
        });
        private ICommand _closeCommand;

        /// <summary>
        /// Command to show the window system menu
        /// </summary>
        public  ICommand MenuCommand => this._menuCommand ??= new RelayCommand(() => {
            SystemCommands.ShowSystemMenu(this._window, this._window.PointToScreen(Mouse.GetPosition(this._window)));
        });
        private ICommand _menuCommand;

        /// <summary>
        /// The current page of application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        /// <summary>
        /// Title of the window
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Minimal window height
        /// </summary>
        public double MinHeight { get; set; }

        /// <summary>
        /// Minimal window width
        /// </summary>
        public double MinWidth { get; set; }

        public Thickness ContentPaddingThickness => new Thickness(this.ContentPadding);
        public double ContentPadding {
            get => this._window.WindowState == WindowState.Maximized ? 0d : this._contentPadding;
            set => this._contentPadding = value;
        } 
        private double _contentPadding;

        /// <summary>
        /// The height of the title bar
        /// </summary>
        public GridLength GridTitleHeight => new GridLength(this.TitleHeight);
        public double TitleHeight { get; set; }

        /// <summary>
        /// Thickness of the border, taking into account the outer margin. Need to resize window and drag & drop
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(this.ResizeBorder + this.OuterMargin);
        private double ResizeBorder => this._window.WindowState == WindowState.Maximized || 
                                       this._dockPosition != WindowDockPosition.Undocked ? 
                                       0d :
                                       6d;

        /// <summary>
        /// Margin around the window to allow for a drop shadow. Need to drag & drop
        /// </summary>
        public Thickness OuterMarginThickness => new Thickness(this.OuterMargin);
        public double OuterMargin {
            get => this._window.WindowState == WindowState.Maximized ? 0d : this._outerMargin;
            set => this._outerMargin = value;
        }
        private double _outerMargin;

        /// <summary>
        /// Radius of window's edges
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(this.WindowRadius);
        public double WindowRadius {
            get => this._window.WindowState == WindowState.Maximized ? 0d : this._windowRadius;
            set => this._windowRadius = value;
        }
        private double _windowRadius;

        /// <summary>
        /// If window resizes updates all property changed events
        /// </summary>
        private void WindowResized() {
            this.OnPropertyChanged(nameof(this.ResizeBorder));
            this.OnPropertyChanged(nameof(this.ResizeBorderThickness));
            this.OnPropertyChanged(nameof(this.OuterMargin));
            this.OnPropertyChanged(nameof(this.OuterMarginThickness));
            this.OnPropertyChanged(nameof(this.WindowRadius));
            this.OnPropertyChanged(nameof(this.WindowCornerRadius));
            this.OnPropertyChanged(nameof(this.TitleHeight));
            this.OnPropertyChanged(nameof(this.GridTitleHeight));
            this.OnPropertyChanged(nameof(this.ContentPadding));
            this.OnPropertyChanged(nameof(this.ContentPaddingThickness));
        }

        /// <summary>
        /// Standard window
        /// </summary>
        private readonly Window _window;

        /// <summary>
        /// Last known dock position
        /// </summary>
        private WindowDockPosition _dockPosition;
    }
}