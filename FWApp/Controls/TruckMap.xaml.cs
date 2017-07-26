using FWViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FWApp.Controls
{
    public sealed partial class TruckMap : UserControl
    {
        public TruckMap()
        {
            this.InitializeComponent();

            // Manhattan
            var Position = new BasicGeoposition()
            {
                Latitude = 40.760333,
                Longitude = -73.981167,
            };
            var Point = new Geopoint(Position);
            Map.Center = Point;
            Map.ZoomLevel = 14;
        }



        public IEnumerable<TruckOpeningVM> Openings
        {
            get { return (IEnumerable<TruckOpeningVM>)GetValue(OpeningsProperty); }
            set { SetValue(OpeningsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Openings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpeningsProperty =
            DependencyProperty.Register("Openings", typeof(IEnumerable<TruckOpeningVM>), typeof(TruckMap), new PropertyMetadata(null, (o, a) => (o as TruckMap).OnOpeningsChanged(a) ));

        private IEnumerable<TruckOpeningVM> _CurrentOpenings;

        private void OnOpeningsChanged(DependencyPropertyChangedEventArgs args)
        {
            Map.Children.Clear();
            if(_CurrentOpenings != null)
            {
                (_CurrentOpenings as INotifyCollectionChanged).CollectionChanged -= Notify_CollectionChanged;
                _CurrentOpenings = null;
            }
            if(args.NewValue != null)
            {
                var Openings = args.NewValue as IEnumerable<TruckOpeningVM>;
                if(Openings != null)
                {
                    _CurrentOpenings = Openings;
                    var Notify = Openings as INotifyCollectionChanged;
                    if(Notify != null)
                        Notify.CollectionChanged += Notify_CollectionChanged;

                    foreach (var o in _CurrentOpenings)
                        AddMarker(o);
                }
            }
        }

        private void Notify_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                Map.Children.Clear();
                foreach (var o in _CurrentOpenings)
                    AddMarker(o);
            }
            else if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var o in e.NewItems.OfType<TruckOpeningVM>())
                    AddMarker(o);
            }
        }

        private void AddMarker(TruckOpeningVM Opening)
        {
            var Marker = new TruckMarker();

            Marker.DataContext = Opening;
            var Position = new BasicGeoposition()
            {
                Latitude = Opening.Latitude,
                Longitude = Opening.Longitude,
            };
            var GeoPoint = new Geopoint(Position);
            MapControl.SetLocation(Marker, GeoPoint);
            MapControl.SetNormalizedAnchorPoint(Marker, new Point(0.5, 0.5));
            Map.Children.Add(Marker);
        }
    }
}
