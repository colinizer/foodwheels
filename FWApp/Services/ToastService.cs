using FWCore.Services;
using FWViewModels.Interfaces;
using NotificationsExtensions;
using NotificationsExtensions.Toasts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace FWApp.Services
{
    public class ToastService : IToastService
    {
        public void DoBasicToast(string Id, string Title, string Message, string ImageUrl)
        {
            ToastContent content = new ToastContent()
            {
                Launch = "Toast:" + Id,

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "ms-appx:///Assets/NewStoreLogo.scale-400.png",
                        },
                        Children =
                        {
                            new AdaptiveText()
                            {
                                HintStyle = AdaptiveTextStyle.Title,
                                Text = Title,
                                HintWrap = true,
                            },

                            new AdaptiveText()
                            {
                                HintStyle = AdaptiveTextStyle.Subtitle,
                                Text = Message,
                                HintWrap = true,
                            },
                            
                            new AdaptiveImage()
                            {
                                Source = TruckService.UrlRoot + "imageresize/?Uri=" + ImageUrl,
                            },                            
                        },
                    }
                },
            };

            var ToastNotification = new ToastNotification(content.GetXml());
            var Notifier = ToastNotificationManager.CreateToastNotifier();
            Notifier.Show(ToastNotification);
        }
    }
}
