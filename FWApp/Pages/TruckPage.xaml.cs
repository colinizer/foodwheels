using FWViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FWApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TruckPage : Page
    {
        public TruckPage()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested -= DataTransferManager_DataRequested;
        }


        private async void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;

            var deferral = args.Request.GetDeferral();

            var VM = this.DataContext as TruckVM;
            request.Data.Properties.Title = VM.Title;
            request.Data.Properties.Description = "Information about this food truck";

            var sb = new StringBuilder();
            sb.AppendLine($"Truck name: {VM.Title}");
            sb.AppendLine($"Food type: {VM.FoodType}");
            sb.AppendLine();
            sb.AppendLine("Opening Times:");
            foreach (var o in VM.Openings)
                sb.AppendLine(o.OpenHoursText);

            request.Data.SetText(sb.ToString());

            var hc = new HttpClient();
            var ImageUrl = "ms-appx:///truck.png";
            var MS = new InMemoryRandomAccessStream();

            using (var Stream = await hc.GetStreamAsync(VM.ImageUrl))
            {
                var OutStream = MS.AsStreamForWrite();
                await Stream.CopyToAsync(OutStream);
                await OutStream.FlushAsync();
            }
            MS.Seek(0);
            var StreamRef = RandomAccessStreamReference.CreateFromStream(MS);
            request.Data.ResourceMap[ImageUrl] = StreamRef;


            sb.Clear();
            sb.AppendLine($"<h1>Truck name: {VM.Title}</h1>");
            sb.AppendLine($"<h2>Food type: {VM.FoodType}</h2>");
            sb.AppendLine($"<img src='{ImageUrl}' width='320' alt=\"Image of {VM.Title}\"/>");
            sb.AppendLine();
            sb.AppendLine("<h2>Opening Times:</h2>");
            sb.AppendLine("<ul>");
            foreach (var o in VM.Openings)
                sb.AppendLine($"<li>{o.OpenHoursText}</li>");
            sb.AppendLine("</ul>");

            var HtmlText = sb.ToString();
            var Html = HtmlFormatHelper.CreateHtmlFormat(HtmlText);
            request.Data.SetHtmlFormat(Html);            

            deferral.Complete();
        }

    }
}
