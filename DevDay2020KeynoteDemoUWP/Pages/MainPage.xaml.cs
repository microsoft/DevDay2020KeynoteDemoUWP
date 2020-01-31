using System;
using WinUI = Microsoft.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.Xaml.Input;
using System.Collections.ObjectModel;
using DevDay2020KeynoteDemoUWP.Model;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Windows.UI.Xaml.Controls;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class MainPage
    {
        public ObservableCollection<Place> PickedPlaces { get; } = new ObservableCollection<Place>();

        public MainPage()
        {
            InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1440, 936);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            ConnectedAnimationService.GetForCurrentView().DefaultDuration = TimeSpan.FromMilliseconds(400);

            if (MainNav.MenuItems[0] is WinUI.NavigationViewItemBase item)
            {
                MainNav.SelectedItem = item;
                NavigateToPage(item.Tag);
            }

            Window.Current.SizeChanged += async (s, e) =>
            {
                await Task.Delay(1200);

                var displayRegionCount = ApplicationView.GetForCurrentView().GetDisplayRegions().Count;
                Debug.WriteLine($"GetDisplayRegions().Count: {displayRegionCount}");

                var windowWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                Debug.WriteLine($"Window width: {windowWidth}");

                if (windowWidth == 1440)
                {
                    Logo.GoToDualScreenState();
                }
                else
                {
                    Logo.GoToSingleScreenState();
                }

                //if (displayRegionCount == 2)
                //{
                //    Logo.GoToDualScreenState();
                //}
                //else
                //{
                //    Logo.GoToSingleScreenState();
                //}
            };
        }

        private void OnMainNavItemInvoked(WinUI.NavigationView sender, WinUI.NavigationViewItemInvokedEventArgs args) =>
            NavigateToPage(args.InvokedItemContainer.Tag);

        private void NavigateToPage(object pageTag)
        {
            var pageName = $"DevDay2020KeynoteDemoUWP.Pages.{pageTag}";
            var pageType = Type.GetType(pageName);

            ContentFrame.Navigate(pageType);
        }

        private void OnPlaceStoreClick(object sender, RoutedEventArgs e)
        {
            PickedPlacesPane.Visibility = Visibility.Visible;
            ContentFrame
                .Fade(0.5f)
                .Scale(scaleX: 0.95f, scaleY: 0.95f, centerX: (float)ContentFrame.ActualWidth / 2, centerY: (float)ContentFrame.ActualHeight / 2)
                .Start();
        }

        private void OnDismissTouchAreaTapped(object sender, TappedRoutedEventArgs e)
        {
            PickedPlacesPane.Visibility = Visibility.Collapsed;
            ContentFrame
                .Fade(1.0f)
                .Scale(scaleX: 1.0f, scaleY: 1.0f, centerX: (float)ContentFrame.ActualWidth / 2, centerY: (float)ContentFrame.ActualHeight / 2)
                .Start();
        }

        private async void OnWunderbarToggleChecked(object sender, RoutedEventArgs e)
        {
            bool modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);

            if (PickedPlaces.Any() && modeSwitched)
            {
                VisualStateManager.GoToState(this, ApplicationViewMode.CompactOverlay.ToString(), false);
            }
        }

        private async void OnWunderbarToggleUnchecked(object sender, RoutedEventArgs e)
        {
            bool modeSwitched = await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.Default);

            if (modeSwitched)
            {
                VisualStateManager.GoToState(this, ApplicationViewMode.Default.ToString(), false);
            }
        }
    }
}
