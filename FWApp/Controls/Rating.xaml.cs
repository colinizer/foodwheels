using FWCore.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
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
    public sealed partial class Rating : UserControl, INotifyPropertyChanged
    {
        public Rating()
        {
            this.InitializeComponent();
        }



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(Rating), new PropertyMetadata(0.0, (o, a) => (o as Rating)?.OnRatingChanged()));





        public ICommand RatingChangedCommand
        {
            get { return (ICommand)GetValue(RatingChangedCommandProperty); }
            set { SetValue(RatingChangedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RatingChangedCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RatingChangedCommandProperty =
            DependencyProperty.Register("RatingChangedCommand", typeof(ICommand), typeof(Rating), new PropertyMetadata(null));




        public event PropertyChangedEventHandler PropertyChanged;

        private void OnRatingChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs("ValueText"));
        }

        public string ValueText => Value.ToString("N1");

        private void FontIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var NewRating = int.Parse((sender as FontIcon).Tag as string);
            if (RatingChangedCommand != null)
                RatingChangedCommand.Execute(NewRating);
        }
    }
}
