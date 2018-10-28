using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Views.PinViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PinSuccessView
    {
        public new PinSuccessViewModel ViewModel => (PinSuccessViewModel)base.ViewModel;

        public PinSuccessView()
        {
            InitializeComponent();
        }

        private void PinSuccessView_OnLoaded(object sender, RoutedEventArgs e)
        {
            //Clear back stack so back navigation to Register flow is prevented
            ClearBackStack();
        }

        private void OnEnterDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ViewModel.ContinueCommand.Execute();
            }
        }
    }
}
