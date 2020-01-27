using DevDay2020KeynoteDemoUWP.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class ComparisonPage : Page
    {
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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var places = (Place[])e.Parameter;
            Place1 = places[0];
            Place2 = places[1];

            var aniamtion1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place1Forward");
            aniamtion1?.TryStart(Place1Image, new UIElement[] { });
            var aniamtion2 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place2Forward");
            aniamtion2?.TryStart(Place2Image, new UIElement[] { });
        }
    }
}
