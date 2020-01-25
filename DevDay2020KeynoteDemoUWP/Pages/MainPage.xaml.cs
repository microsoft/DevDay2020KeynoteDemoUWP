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
