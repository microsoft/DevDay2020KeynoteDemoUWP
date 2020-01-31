using DevDay2020KeynoteDemoUWP.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class ComparisonPage : Page
    {
        private Image _selectedPlaceImage;
        public Place Place1 { get; set; }
        public Place Place2 { get; set; }

        public ComparisonPage()
        {
            InitializeComponent();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("place1Backward", Place1Image);
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("place2Backward", Place2Image);
            }
            else if (e.NavigationMode == NavigationMode.New)
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("comparisonToDetail", _selectedPlaceImage);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                var places = (Place[])e.Parameter;
                Place1 = places[0];
                Place2 = places[1];

                var aniamtion1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place1Forward");
                aniamtion1?.TryStart(Place1Image);
                var aniamtion2 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place2Forward");
                aniamtion2?.TryStart(Place2Image);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                var aniamtion = ConnectedAnimationService.GetForCurrentView().GetAnimation("detailToComparison");
                aniamtion?.TryStart(_selectedPlaceImage);
            }
        }

        private void OnSelectPlace1Click(object sender, RoutedEventArgs e)
        {
            _selectedPlaceImage = Place1Image;
            Frame.Navigate(typeof(DetailPage), Place1);
        }

        private void OnSelectPlace2Click(object sender, RoutedEventArgs e)
        {
            _selectedPlaceImage = Place2Image;
            Frame.Navigate(typeof(DetailPage), Place2);
        }

        private void ArrangeForDualScreenTall()
        {
        }

        private void ArrangeForDualScreenWide()
        {
        }

        private void ArrangeForSingleScreen()
        {
        }

        /// <summary>
        /// STEP 2.2: For complex layouts, manually switch between UI based on postures.
        /// </summary>
        private void OnTwoPaneViewModeChanged(WinUI.TwoPaneView sender, object args)
        {
            switch (sender.Mode)
            {
                // Update layout for viewing on a Single display.
                case WinUI.TwoPaneViewMode.SinglePane:
                    ArrangeForSingleScreen();
                    break;

                // Update layout for viewing on a Double Portrait display
                case WinUI.TwoPaneViewMode.Wide:
                    ArrangeForDualScreenWide();
                    break;

                // Update layout for viewing on a Double Landscape display
                case WinUI.TwoPaneViewMode.Tall:
                    ArrangeForDualScreenTall();
                    break;
            }
        }
    }
}