﻿using Windows.System;
using Windows.UI.Xaml.Input;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.ViewModels.PinViewModels;

namespace Excalibur.Tests.Encrypted.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginView
    {
        public new LoginViewModel ViewModel => (LoginViewModel)base.ViewModel;

        public LoginView()
        {
            InitializeComponent();
        }

        private void OnEnterDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                ViewModel.LoginCommand.Execute();
            }
        }
    }
}
