using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Gibbo.Editor.WPF
{
    public class RoundedButtonToggle : ToggleButton
    {
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius),
            typeof(RoundedButtonToggle), new UIPropertyMetadata());
    }
}
