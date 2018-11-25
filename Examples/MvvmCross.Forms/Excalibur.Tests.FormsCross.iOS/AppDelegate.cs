using Foundation;
using MvvmCross.Forms.Platforms.Ios.Core;
using UIKit;

namespace Excalibur.Tests.FormsCross.Ios
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<MvxFormsIosSetup<Core.App, Core.Ui.App>, Core.App, Core.Ui.App>
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(73, 9, 1);
            UINavigationBar.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);

            return base.FinishedLaunching(app, options);
        }
    }
}
