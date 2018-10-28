using Windows.System;
using Windows.UI.Xaml.Input;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Views.PinViews
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPinView
    {
        public new RegisterPinViewModel ViewModel => (RegisterPinViewModel)base.ViewModel;

        public RegisterPinView()
        {
            InitializeComponent();
        }

        private void OnEnterDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter && ViewModel.PinValid)
            {
                ViewModel.ContinueCommand.Execute();
            }
        }
    }
}
