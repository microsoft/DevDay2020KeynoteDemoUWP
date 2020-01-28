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
                if (e.SourcePageType == typeof(DestinationsPage))
                {
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardToMain", HeroImage);
                    //animation.Configuration = new DirectConnectedAnimationConfiguration();
                }
                else if (e.SourcePageType == typeof(ComparisonPage))
                {
                    ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("detailToComparison", HeroImage);
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            SelectedPlace = e.Parameter as Place;

            var aniamtion1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("mainToDetail");
            aniamtion1?.TryStart(HeroImage, new UIElement[] { Header });

            var aniamtion2 = ConnectedAnimationService.GetForCurrentView().GetAnimation("comparisonToDetail");
            aniamtion2?.TryStart(HeroImage, new UIElement[] { Header });
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
