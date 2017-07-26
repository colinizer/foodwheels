using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FWApp.Controls
{
    public sealed partial class TruckMarker : UserControl
    {
        public TruckMarker()
        {
            this.InitializeComponent();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var uiSender = sender as UIElement;
            var flyout = (FlyoutBase)uiSender.GetValue(FlyoutBase.AttachedFlyoutProperty);
            flyout.ShowAt(uiSender as FrameworkElement);
        }

        private Point CalcOffsets(UIElement elem)
        {
            // I don't recall the exact specifics on why these
            // calls are needed - but this works.
            var transform = elem.TransformToVisual(this);
            Point point = transform.TransformPoint(new Point(0, 0));

            return point;
        }
    }
}
