using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FWApp.Services
{
    public class DialogService : IDialogService
    {
        public async Task<int> ShowDialogAsync(string title, string caption, string button1, string button2)
        {
            var cd = new ContentDialog()
            {
                Title = title,
                Content = caption,
                PrimaryButtonText = button1 ?? "",
                SecondaryButtonText = button2 ?? "",
            };

            var result = await cd.ShowAsync();

            if (result == ContentDialogResult.None)
                return 0;
            else if (result == ContentDialogResult.Primary)
                return 1;
            else
                return 2;
        }
    }
}
