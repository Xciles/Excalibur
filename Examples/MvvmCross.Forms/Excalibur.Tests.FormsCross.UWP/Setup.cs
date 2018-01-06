using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Platform;
using MvvmCross.Forms.Uwp;
using MvvmCross.Platform.Logging;

namespace Excalibur.Tests.FormsCross.UWP
{
    public class Setup : MvxFormsWindowsSetup
    {
        public Setup(Frame rootFrame, LaunchActivatedEventArgs e) : base(rootFrame, e)
        {
        }
        protected override MvxLogProviderType GetDefaultLogProviderType() => MvxLogProviderType.None;

        protected override IMvxApplication CreateApp()
        {
            return new Excalibur.Tests.FormsCross.App();
        }

        protected override MvxFormsApplication CreateFormsApplication()
        {
            return new FormsApp();
        }
    }
}