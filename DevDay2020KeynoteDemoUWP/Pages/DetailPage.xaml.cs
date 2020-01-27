using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP.Pages
{
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            InitializeComponent();
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }
    }
}
