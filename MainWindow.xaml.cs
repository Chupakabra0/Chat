﻿namespace Chat {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow() {
            InitializeComponent();
            this.DataContext = new WindowVM(this);
        }
    }
}