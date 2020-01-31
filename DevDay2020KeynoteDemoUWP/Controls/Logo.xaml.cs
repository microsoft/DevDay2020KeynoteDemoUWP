using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DevDay2020KeynoteDemoUWP.Controls
{
    public sealed partial class Logo : Page
    {
        public Logo()
        {
            InitializeComponent();
        }

        public void GoToSingleScreenState() =>
            VisualStateManager.GoToState(this, "SingleScreen", true);

        public void GoToDualScreenState() =>
            VisualStateManager.GoToState(this, "DualScreen", true);

        public void SetAngle(double angle)
        {
            GreyTriangleProjection.RotationX = -angle;
            BlueTriangleProjection.RotationX = angle;
        }
    }
}
