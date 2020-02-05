using System;
using DevDay2020KeynoteDemoUWP.Model;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class WonderbarPage
    {
        private readonly AppWindow _appWindow;
        private readonly List<Place> _places = App.GetGroupedPlaces().SelectMany(p => p).ToList();

        public WonderbarPage(AppWindow appWindow)
        {
            InitializeComponent();

            _appWindow = appWindow;
        }

        private async void OnCloseClick(object sender, RoutedEventArgs e) => 
            await _appWindow.CloseAsync();
    }
}
