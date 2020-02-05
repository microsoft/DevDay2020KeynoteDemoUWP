using DevDay2020KeynoteDemoUWP.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Extensions;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

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
                    var gridViewItem = (GridViewItem)MainGridView.ContainerFromItem(place);
                    var selectionToggle = gridViewItem.FindDescendant<ToggleButton>();
                    var centerX = (float)selectionToggle.ActualWidth / 2;
                    var centerY = (float)selectionToggle.ActualHeight / 2;
            
                    if (place.IsSelected)
                    {
                        _placesToCompare.Add(place);
                        selectionToggle
                            .Offset(offsetX: -92.0f, offsetY: -50.0f)
                            .Scale(scaleX: 1.5f, scaleY: 1.5f, centerX: centerX, centerY: centerY)
                            .Start();
                    }
                    else
                    {
                        _placesToCompare.Remove(place);
                        selectionToggle
                            .Offset(offsetX: 0.0f, offsetY: 0.0f)
                            .Scale(scaleX: 1.0f, scaleY: 1.0f, centerX: centerX, centerY: centerY)
                            .Start();
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
                    Wonderbar.Visibility = _placesToCompare.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
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
            MainGridView.PrepareConnectedAnimation("mainToDetail", e.ClickedItem, "PlaceImage");

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

        private async void OnWonderbarClick(object sender, RoutedEventArgs e)
        {
            // 1. Create a new Window.
            var appWindow = await AppWindow.TryCreateAsync();

            // 2. Create the page and set the new window's content..
            ElementCompositionPreview.SetAppWindowContent(appWindow, new WonderbarPage(appWindow));

            // 3. Check if you can leverage the compact overlay APIs.
            if (appWindow.Presenter.IsPresentationSupported(AppWindowPresentationKind.CompactOverlay))
            {
                // 4. Show the window.
                await appWindow.TryShowAsync();

                // 5. If so, change that window to be inside the compact overlay region.
                appWindow.Presenter.RequestPresentation(AppWindowPresentationKind.CompactOverlay);
            }
        }

        private async void OnRootDragStarting(UIElement sender, DragStartingEventArgs args)
        {
            var deferal = args.GetDeferral();

            if (((Grid)sender).DataContext is Place place)
            {
                args.Data.RequestedOperation = DataPackageOperation.Copy;

                //args.Data.SetData(StandardDataFormats.Text, place.CityName);

                var imageUri = new Uri($"ms-appx://{place.ImageUri}", UriKind.RelativeOrAbsolute);
                var file = await StorageFile.GetFileFromApplicationUriAsync(imageUri);
                args.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
                args.DragUI.SetContentFromBitmapImage(new BitmapImage(imageUri) { DecodePixelWidth = 240 });
            }

            deferal.Complete();
        }
    }
}
