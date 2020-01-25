using System;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
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
