using FWCore.Interfaces;
using FWCore.Utils;
using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

namespace FWApp.Services
{
    public class NavigationService : INavigationService
    {
        public void SetFrame(Frame Frame)
        {
            _Frame = Frame;

            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                GoBack();
            };
        }

        Frame _Frame;

        public string CurrentPageName
        {
            get
            {
                return _Frame.Content?.GetType().Name ?? "";
            }
        }

        public void GoBack()
        {
            if(_Frame.CanGoBack)
                _Frame.GoBack();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = _Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        public async Task SaveFrame()
        {
            if(_Frame != null)
            {
                var FrameState = _Frame.GetNavigationState();
                var AFS = Repository.GetObject<IAppFileService>();
                await AFS.CreateOrReplaceFile(StateFileName, FrameState);
            }
        }

        string StateFileName = "framestate.txt";

        public async Task RestoreFrame()
        {
            var AFS = Repository.GetObject<IAppFileService>();
            var FrameState = await AFS.ReadFile(StateFileName);
            _Frame.SetNavigationState(FrameState);

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = _Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        public void GotoPage(string PageTypeName)
        {
            if (String.Compare(CurrentPageName, PageTypeName, true) == 0)
                return;
            var PageType = typeof(NavigationService).GetTypeInfo().Assembly.DefinedTypes.Where(t => t.FullName.EndsWith(PageTypeName, StringComparison.OrdinalIgnoreCase)).Select(t => t.AsType()).FirstOrDefault();
            _Frame.Navigate(PageType, "wibble");

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = _Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            if (NavigatedForward != null)
                NavigatedForward();
        }

        public event Action NavigatedForward;
    }
}
