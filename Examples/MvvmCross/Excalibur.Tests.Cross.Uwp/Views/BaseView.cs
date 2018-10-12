using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using MvvmCross.Platforms.Uap.Views;

namespace Excalibur.Tests.Cross.Uwp.Views
{
    public class BaseView : MvxWindowsPage
    {
        protected BaseView()
        {
            SystemNavigationManager.GetForCurrentView().BackRequested += App_BackRequested;
        }

        private void App_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            var mainView = rootFrame.Content as MainView;
            if ((mainView?.CanGoBack() ?? false) && e.Handled == false)
            {
                e.Handled = true;
                mainView.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame == null)
            {
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                return;
            }

            var mainView = rootFrame.Content as MainView;

            if (mainView?.CanGoBack() ?? false)
            {
                // Show UI in title bar if opted-in and in-app backstack is not empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            }
            else
            {
                // Remove the UI from the title bar if in-app back stack is empty.
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }
    }
}
