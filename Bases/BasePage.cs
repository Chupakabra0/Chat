using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Chat.Animations;

namespace Chat.Pages {
    /// <summary>
    /// Basic class for all pages
    /// </summary>
    public class BasePage : Page {
        /// <summary>
        /// Default page constructor
        /// </summary>
        public BasePage() {
            if (this.PageLoadAnimation != PageAnimation.None) {
                this.Visibility = Visibility.Collapsed;
            }

            this.Loaded += this.OnLoaded;
        }

        /// <summary>
        /// Animation that plays when page is loading
        /// </summary>
        public PageAnimation PageLoadAnimation { get; set; } = PageAnimation.SlideAndFadeFromRight;

        /// <summary>
        /// Animation that plays when page is unloading
        /// </summary>
        public PageAnimation PageUnloadAnimation { get; set; } = PageAnimation.SlideAndFadeFromLeft;

        /// <summary>
        /// The time any slide animation takes
        /// </summary>
        public Duration SlideSecondsDuration => new Duration(TimeSpan.FromSeconds(this.SlideSeconds));
        private double SlideSeconds { get; } = 0.8;

        /// <summary>
        /// Speed of animation
        /// </summary>
        public double DecelerationSpeed { get; set; } = 0.9;

        /// <summary>
        /// Once page is loaded perform needed animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnLoaded(object sender, RoutedEventArgs e) => await this.AnimateIn();

        private async Task AnimateIn() {
            switch (this.PageLoadAnimation) {
                case PageAnimation.None: {
                    return;
                }
                case PageAnimation.SlideAndFadeFromRight: {
                    var storyBoard = new Storyboard();
                    var slideAnimation = new ThicknessAnimation {
                        Duration = this.SlideSecondsDuration,
                        From = new Thickness(this.WindowWidth, 0.0, -this.WindowWidth, -0.0),
                        To = new Thickness(0.0),
                        DecelerationRatio = this.DecelerationSpeed
                    };
                    Storyboard.SetTargetProperty(slideAnimation, new PropertyPath("Margin"));
                    storyBoard.Children.Add(slideAnimation);
                    storyBoard.Begin(this);

                    this.Visibility = Visibility.Visible;

                    await Task.Delay(slideAnimation.Duration.TimeSpan);
                    break;
                }
                case PageAnimation.SlideAndFadeFromLeft: {
                    break;
                }
                default: {
                    throw new NotImplementedException();
                }
            }

        }
    }
}