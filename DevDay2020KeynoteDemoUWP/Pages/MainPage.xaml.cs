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
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.UI.Core;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class MainPage
    {
        public ObservableCollection<Place> PickedPlaces { get; } = new ObservableCollection<Place>();

        private HingeAngleSensor _sensor;

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

                //var displayRegionCount = ApplicationView.GetForCurrentView().GetDisplayRegions().Count;
                //Debug.WriteLine($"GetDisplayRegions().Count: {displayRegionCount}");

                var windowWidth = ApplicationView.GetForCurrentView().VisibleBounds.Width;
                //Debug.WriteLine($"Window width: {windowWidth}");

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

            Loaded += async (s, e) =>
            {
                Logo.Start();

                _sensor = await HingeAngleSensor.GetDefaultAsync();

                if (_sensor != null)
                {
                    _sensor.ReportThresholdInDegrees = _sensor.MinReportThresholdInDegrees;

                    _sensor.ReadingChanged += OnSensorReadingChanged;
                    var current = (await _sensor.GetCurrentReadingAsync()).AngleInDegrees;
                }

                async void OnSensorReadingChanged(HingeAngleSensor sender, HingeAngleSensorReadingChangedEventArgs args)
                {
                    // Event is invoked from a different thread.
                    await Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                    {
                        // Range should be set between -80 and 80.
                        var angle = args.Reading.AngleInDegrees / 2 - 90;
                        if (angle < -80)
                        {
                            angle = -80;
                        }
                        else if (angle > 80)
                        {
                            angle = 80;
                        }

                        Logo.SetAngle(angle);
                    });
                }
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
