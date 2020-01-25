using DevDay2020KeynoteDemoUWP.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DestinationsPage : Page
    {
        private readonly ObservableCollection<GroupInfoList> _list = GetGroupedPlaces();
        private readonly ObservableCollection<Place> _selectedPlaces = new ObservableCollection<Place>();

        public static ObservableCollection<GroupInfoList> GetGroupedPlaces()
        {
            var group1 = new GroupInfoList { Key = "Architecture" };
            group1.Add(new Place("", "/Assets/Images/bantersnaps-wPMvPMD9KBI-unsplash.png"));
            group1.Add(new Place("", "/Assets/Images/eva-dang-EXdXLrZXS9Q-unsplash.png"));
            group1.Add(new Place("", "/Assets/Images/tomas-nozina-UP22zkjJGZo-unsplash.png"));

            var group2 = new GroupInfoList { Key = "Outdoor" };
            group2.Add(new Place("", "/Assets/Images/ashim-d-silva-WeYamle9fDM-unsplash.png"));
            group2.Add(new Place("", "/Assets/Images/annie-spratt-tB4Gf7ddcJY-unsplash.png"));
            group2.Add(new Place("", "/Assets/Images/damian-patkowski-QeC4oPdKu7c-unsplash.png"));
            group2.Add(new Place("", "/Assets/Images/willian-west-YpKiwlvhOpI-unsplash.png"));
            group2.Add(new Place("", "/Assets/Images/felix-NAytNmKtyiU-unsplash.png"));
            group2.Add(new Place("", "/Assets/Images/willian-west-TVyjcTEKHLU-unsplash.png"));

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
                    }
                };

            }
        }
    }
}
