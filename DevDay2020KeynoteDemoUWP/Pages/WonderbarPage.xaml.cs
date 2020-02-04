using System;
using DevDay2020KeynoteDemoUWP.Model;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;
using Windows.UI.WindowManagement;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class WonderbarPage
    {
        private readonly List<Place> _places = App.GetGroupedPlaces().SelectMany(p => p).ToList();

        public WonderbarPage()
        {
            InitializeComponent();
        }

        private async void OnWunderbarClick(object sender, RoutedEventArgs e)
        {
            var appWindow = await AppWindow.TryCreateAsync();
            ElementCompositionPreview.SetAppWindowContent(appWindow, new MainPage());
            await appWindow.TryShowAsync();

            appWindow.Presenter.RequestPresentation(AppWindowPresentationKind.Default);
        }
    }
}
