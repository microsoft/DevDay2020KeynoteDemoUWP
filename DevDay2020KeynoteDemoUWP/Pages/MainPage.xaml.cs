using System;
using WinUI = Microsoft.UI.Xaml.Controls;
using Windows.UI.ViewManagement;
using Windows.Foundation;
using Windows.UI.Xaml.Shapes;

namespace DevDay2020KeynoteDemoUWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size(1440, 936);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

            if (MainNav.MenuItems[0] is WinUI.NavigationViewItemBase item)
            {
                MainNav.SelectedItem = item;
                NavigateToPage(item.Tag);
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
