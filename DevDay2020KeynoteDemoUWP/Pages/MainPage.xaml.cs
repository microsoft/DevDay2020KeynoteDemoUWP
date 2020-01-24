using System;
using Windows.UI.Xaml;
using Windows.UI.WindowManagement;
using Windows.UI.Xaml.Hosting;
using WinUI = Microsoft.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (MainNav.MenuItems[0] is WinUI.NavigationViewItemBase item)
            {
                MainNav.SelectedItem = item;
                NavigateToPage(item.Tag);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // 1. Create a new Window
            AppWindow appWindow = await AppWindow.TryCreateAsync();

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

        private void OnMainNavItemInvoked(WinUI.NavigationView sender, WinUI.NavigationViewItemInvokedEventArgs args) =>
            NavigateToPage(args.InvokedItemContainer.Tag);

        private void NavigateToPage(object pageTag)
        {
            var pageName = $"DevDay2020KeynoteDemoUWP.Pages.{pageTag}";
            var pageType = Type.GetType(pageName);

            ContentFrame.Navigate(pageType);
        }
    }
}
