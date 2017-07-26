using FWApp.Services;
using FWCore.Interfaces;
using FWCore.Utils;
using FWViewModels;
using FWViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FWApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.Resuming += App_Resuming;
            this.EnteredBackground += App_EnteredBackground;

            var DS = new DialogService();
            Repository.StoreFactory<IDialogService>(() => DS);

            var KEH = new KnownExceptionHandler();
            Repository.StoreFactory<IKnownExceptionHandler>(() => KEH);

            var NS = new NavigationService();
            Repository.StoreFactory<INavigationService>(() => NS);

            var TPS = new TilePinService();
            Repository.StoreFactory<ITilePinService>(() => TPS);

            var TS = new ToastService();
            Repository.StoreFactory<IToastService>(() => TS);

            var AFS = new AppFileService();
            Repository.StoreFactory<IAppFileService>(() => AFS);

            var AMS = new AppMetadataService();
            Repository.StoreFactory<IAppMetadata>(() => AMS);
        }

        private async void App_EnteredBackground(object sender, EnteredBackgroundEventArgs e)
        {
            var Deferral = e.GetDeferral();

            await ViewModelLocator.MainVM.EnteredBackground();
            await Repository.GetObject<INavigationService>()?.SaveFrame();

            Deferral.Complete();
        }

        private void App_Resuming(object sender, object e)
        {
            ViewModelLocator.MainVM.Resuming();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }
            
            if (e.PrelaunchActivated == false)
            {
                if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
                {
                    var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                    if (titleBar != null)
                    {
                        var EdgingColor = (Color)this.Resources["EdgingColor"];
                        var EdgingContrastColor = (Color)this.Resources["EdgingContrastColor"];
                        titleBar.ButtonBackgroundColor = EdgingColor;
                        titleBar.ButtonForegroundColor = EdgingContrastColor;
                        titleBar.BackgroundColor = EdgingColor;
                        titleBar.ForegroundColor = EdgingContrastColor;
                    }
                }
                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));


                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    await Repository.GetObject<INavigationService>()?.RestoreFrame();
                }

                // Ensure the current window is active
                Window.Current.Activate();
                await InstallVoiceCommands();

                bool WasRunning = e.PreviousExecutionState == ApplicationExecutionState.Running
                    || e.PreviousExecutionState == ApplicationExecutionState.Suspended
                    || e.PreviousExecutionState == ApplicationExecutionState.Terminated;
                bool LoadData = !(e.PreviousExecutionState == ApplicationExecutionState.Running
                    || e.PreviousExecutionState == ApplicationExecutionState.Suspended);
                ViewModelLocator.MainVM.Launch(WasRunning, LoadData, e.Arguments);
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args.Kind == ActivationKind.VoiceCommand || args.Kind == ActivationKind.ToastNotification)
            {
                Frame rootFrame = Window.Current.Content as Frame;

                // Do not repeat app initialization when the Window already has content,
                // just ensure that the window is active
                if (rootFrame == null)
                {
                    // Create a Frame to act as the navigation context and navigate to the first page
                    rootFrame = new Frame();

                    rootFrame.NavigationFailed += OnNavigationFailed;

                    if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        //TODO: Load state from previously suspended application
                    }

                    // Place the frame in the current Window
                    Window.Current.Content = rootFrame;
                }

                ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));

                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(MainPage), null);
                }
                // Ensure the current window is active
                Window.Current.Activate();

                bool WasRunning = args.PreviousExecutionState == ApplicationExecutionState.Running
                    || args.PreviousExecutionState == ApplicationExecutionState.Suspended
                    || args.PreviousExecutionState == ApplicationExecutionState.Terminated;
                bool LoadData = !(args.PreviousExecutionState == ApplicationExecutionState.Running
                    || args.PreviousExecutionState == ApplicationExecutionState.Suspended);


                if (args.Kind == ActivationKind.VoiceCommand)
                {
                    var VoiceArgs = args as VoiceCommandActivatedEventArgs;
                    var Result = VoiceArgs.Result;

                    var Rule = Result.RulePath?.FirstOrDefault() ?? "";
                    var VoiceArg = "Voice:" + Rule;
                    IReadOnlyList<string> FoodTypes = null;
                    if (Result.SemanticInterpretation.Properties.TryGetValue("foodtype", out FoodTypes))
                        VoiceArg += ":" + FoodTypes?.FirstOrDefault() ?? "";
                    ViewModelLocator.MainVM.Launch(WasRunning, LoadData, VoiceArg);
                }
                else if(args.Kind == ActivationKind.ToastNotification)
                {
                    var ToastArgs = args as ToastNotificationActivatedEventArgs;
                    ViewModelLocator.MainVM.Launch(WasRunning, LoadData, ToastArgs.Argument);
                }
            }
        }

        private async Task InstallVoiceCommands()
        {
            try
            {
                // Install the main VCD. 
                StorageFile vcdStorageFile =
                  await Package.Current.InstalledLocation.GetFileAsync(
                    @"VoiceCommands.xml");

                await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.
                  InstallCommandDefinitionsFromStorageFileAsync(vcdStorageFile);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
