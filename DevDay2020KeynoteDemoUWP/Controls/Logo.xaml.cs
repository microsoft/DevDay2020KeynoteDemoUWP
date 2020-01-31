using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP.Controls
{
    public sealed partial class Logo : Page
    {
        public Logo()
        {
            InitializeComponent();

            //Test();
        }

        private async void Test()
        {
            await Task.Delay(4000);
            GoToDualScreenState();
            await Task.Delay(4000);
            GoToSingleScreenState();
            await Task.Delay(4000);
            GoToDualScreenState();
            await Task.Delay(4000);
            GoToSingleScreenState();
        }

        public async void GoToSingleScreenState()
        {
            VisualStateManager.GoToState(this, "RestoreAngle", true);
            await Task.Delay(400);

            AnimatedBlueTriangle.Visibility = Visibility.Visible;
            AnimatedGreyTriangle.Visibility = Visibility.Visible;
            BlueTriangle.Visibility = Visibility.Collapsed;
            GreyTriangle.Visibility = Visibility.Collapsed;

            VisualStateManager.GoToState(this, "SingleScreen", true);
        }

        public async void GoToDualScreenState()
        {
            VisualStateManager.GoToState(this, "DualScreen", true);
            await Task.Delay(400);

            BlueTriangle.Visibility = Visibility.Visible;
            GreyTriangle.Visibility = Visibility.Visible;
            AnimatedBlueTriangle.Visibility = Visibility.Collapsed;
            AnimatedGreyTriangle.Visibility = Visibility.Collapsed;
        }
    }
}
