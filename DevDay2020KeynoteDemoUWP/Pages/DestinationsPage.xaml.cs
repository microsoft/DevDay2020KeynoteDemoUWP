using DevDay2020KeynoteDemoUWP.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Animations;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DestinationsPage : Page
    {
        private readonly ObservableCollection<GroupInfoList> _places = App.GetGroupedPlaces();
        private readonly ObservableCollection<Place> _placesToCompare = new ObservableCollection<Place>();
        private Place _selectedPlace;

        public DestinationsPage()
        {
            InitializeComponent();

            ManageGridViewItemSelections();
        }

        private void ManageGridViewItemSelections()
        {
            foreach (var place in _places.SelectMany(p => p))
            {
                place.PropertyChanged += (s, e) =>
                {
                    if (place.IsSelected)
                    {
                        _placesToCompare.Add(place);
                    }
                    else
                    {
                        _placesToCompare.Remove(place);
                    }

                    if (_placesToCompare.Count >= 2)
                    {
                        foreach (var item in MainGridView.Items)
                        {
                            if (item is Place p && !p.IsSelected)
                            {
                                if (MainGridView.ContainerFromItem(item) is GridViewItem element)
                                {
                                    element.IsEnabled = false;
                                }
                            }
                        }

                        CommandBar.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        foreach (var item in MainGridView.Items)
                        {
                            if (MainGridView.ContainerFromItem(item) is GridViewItem element)
                            {
                                element.IsEnabled = true;
                            }
                        }

                        CommandBar.Visibility = Visibility.Collapsed;
                    }

                    MainGridView.IsItemClickEnabled = _placesToCompare.Count == 0;
                    Wunderbar.Visibility = _placesToCompare.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                };

            }
        }

        private void OnCompareClick(object sender, RoutedEventArgs e)
        {
            var place1 = _placesToCompare[0];
            var place2 = _placesToCompare[1];

            MainGridView.PrepareConnectedAnimation("place1Forward", place1, "PlaceImage");
            MainGridView.PrepareConnectedAnimation("place2Forward", place2, "PlaceImage");

            Frame.Navigate(typeof(ComparisonPage), new Place[] { place1, place2 });
        }

        private void OnMainGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            _selectedPlace = e.ClickedItem as Place;
            MainGridView.PrepareConnectedAnimation("forwardToDetail", e.ClickedItem, "PlaceImage");

            //Frame.Navigate(typeof(DetailPage), _selectedPlace, new SuppressNavigationTransitionInfo());
            Frame.Navigate(typeof(DetailPage), _selectedPlace);
        }

        private async void OnMainGridViewLoaded(object sender, RoutedEventArgs e)
        {
            if (_selectedPlace != null)
            {
                MainGridView.ScrollIntoView(_selectedPlace);
                MainGridView.UpdateLayout();

                var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("backwardToMain");
                if (animation != null)
                {
                    await MainGridView.TryStartConnectedAnimationAsync(animation, _selectedPlace, "PlaceImage");
                    _selectedPlace = null;
                }
            }
            else if (_placesToCompare.Count == 2)
            {
                MainGridView.ScrollIntoView(_placesToCompare[1]);
                MainGridView.UpdateLayout();

                var animation1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place1Backward");
                if (animation1 != null)
                {
                    await MainGridView.TryStartConnectedAnimationAsync(animation1, _placesToCompare[0], "PlaceImage");
                }

                var animation2 = ConnectedAnimationService.GetForCurrentView().GetAnimation("place2Backward");
                if (animation1 != null)
                {
                    await MainGridView.TryStartConnectedAnimationAsync(animation2, _placesToCompare[1], "PlaceImage");
                }
            }
        }

        private async void OnWunderbarClick(object sender, RoutedEventArgs e)
        {
            // 1. Create a new Window.
            var appWindow = await AppWindow.TryCreateAsync();

            // 2. Create the pageand set the new window's content..
            ElementCompositionPreview.SetAppWindowContent(appWindow, new WonderbarPage());

            // 3. Check if you can leverage the compact overlay APIs
            if (appWindow.Presenter.IsPresentationSupported(AppWindowPresentationKind.CompactOverlay))
            {
                // 4. Show the window.
                await appWindow.TryShowAsync();

                // 5. If so, change that window to be inside the compact overlay region.
                appWindow.Presenter.RequestPresentation(AppWindowPresentationKind.CompactOverlay);
            }
        }

        private void OnMainGridViewContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
        {
            var animations = (AnimationCollection)App.Current.Resources["ImplicitOffsetAnimation"];
            Implicit.SetAnimations(args.ItemContainer, animations);
        }
    }
}
