using System;
using Windows.UI.Xaml.Controls;
using Excalibur.Tests.Cross.Uwp.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Logging;
using MvvmCross.Platform.Platform;
using MvvmCross.Uwp.Platform;
using MvvmCross.Uwp.Views;

namespace Excalibur.Tests.Cross.Uwp
{
    public class Setup : MvxWindowsSetup
    {
        public Setup(Frame rootFrame) : base(rootFrame)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override IMvxWindowsViewPresenter CreateViewPresenter(IMvxWindowsFrame rootFrame)
        {
            return new CustomViewPresenter(rootFrame);
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override MvxLogProviderType GetDefaultLogProviderType()
        {
            return MvxLogProviderType.None;
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint());
        }
    }
}
