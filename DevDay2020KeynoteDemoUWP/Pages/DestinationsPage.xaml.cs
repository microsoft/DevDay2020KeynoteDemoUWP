using DevDay2020KeynoteDemoUWP.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DestinationsPage : Page
    {
        private readonly ObservableCollection<GroupInfoList> _list = GetGroupedPlaces();
        private readonly ObservableCollection<Place> _selectedPlaces = new ObservableCollection<Place>();

        public static ObservableCollection<GroupInfoList> GetGroupedPlaces()
        {
            var group1 = new GroupInfoList { Key = "Architecture" };
            group1.Add(new Place("", "/Assets/Images/bantersnaps-wPMvPMD9KBI-unsplash.jpg"));
            group1.Add(new Place("", "/Assets/Images/eva-dang-EXdXLrZXS9Q-unsplash.jpg"));
            group1.Add(new Place("", "/Assets/Images/tomas-nozina-UP22zkjJGZo-unsplash.jpg"));

            var group2 = new GroupInfoList { Key = "Outdoor" };
            group2.Add(new Place("", "/Assets/Images/ashim-d-silva-WeYamle9fDM-unsplash.jpg"));
            group2.Add(new Place("", "/Assets/Images/annie-spratt-tB4Gf7ddcJY-unsplash.jpg"));
            group2.Add(new Place("", "/Assets/Images/damian-patkowski-QeC4oPdKu7c-unsplash.jpg"));
            group2.Add(new Place("", "/Assets/Images/willian-west-YpKiwlvhOpI-unsplash.jpg"));
            group2.Add(new Place("", "/Assets/Images/felix-NAytNmKtyiU-unsplash.jpg"));
            group2.Add(new Place("", "/Assets/Images/willian-west-TVyjcTEKHLU-unsplash.jpg"));

            var groups = new ObservableCollection<GroupInfoList>();
            groups.Add(group1);
            groups.Add(group2);

            return groups;
        }

        public DestinationsPage()
        {
            InitializeComponent();

            PlacesSource.Source = _list;

            ManageGridViewItemSelections();
        }

        private void ManageGridViewItemSelections()
        {
            foreach (var place in _list.SelectMany(p => p))
            {
                place.PropertyChanged += (s, e) =>
                {
                    if (place.IsSelected)
                    {
                        _selectedPlaces.Add(place);
                    }
                    else
                    {
                        _selectedPlaces.Remove(place);
                    }

                    if (_selectedPlaces.Count >= 2)
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
                };

            }
        }

        private void OnCompareClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ComparisonPage));
        }

        private void OnMainGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(DetailPage));
        }

        private async void OnOpenWunderbarClick(object sender, RoutedEventArgs e)
        {
            // 1. Create a new Window
            var appWindow = await AppWindow.TryCreateAsync();

            // 2. Create the pageand set the new window's content
            ElementCompositionPreview.SetAppWindowContent(appWindow, new WonderbarPage());

            // 3. Check if you can leverage the compact overlay APIs
            if (appWindow.Presenter.IsPresentationSupported(AppWindowPresentationKind.CompactOverlay))
            {
                // 4. Show the window
                await appWindow.TryShowAsync();

                // 5. If so, change that window to be inside the compact overlay region
                appWindow.Presenter.RequestPresentation(AppWindowPresentationKind.CompactOverlay);
            }
        }
    }
}
