using DevDay2020KeynoteDemoUWP.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DetailPage : Page
    {
        public Place SelectedPlace { get; set; }

        public DetailPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardToMain", HeroImage);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SelectedPlace = e.Parameter as Place;

            var aniamtion = ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardToDetail");
            aniamtion?.TryStart(HeroImage, new UIElement[] { Header, Pane2Panel });
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
