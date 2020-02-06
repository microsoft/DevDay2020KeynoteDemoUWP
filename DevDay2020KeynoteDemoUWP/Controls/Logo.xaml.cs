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

        public void Start() =>
            StartupAnimation.Begin();

        public void GoToSingleScreenState() =>
            VisualStateManager.GoToState(this, nameof(SingleScreen), true);

        public void GoToDualScreenState() =>
            VisualStateManager.GoToState(this, nameof(DualScreen), true);

        public void SetAngle(double angle)
        {
            GreyTriangleProjection.RotationX = -angle;
            BlueTriangleProjection.RotationX = angle;
        }
    }
}
