using Android.App;
using Android.Content.PM;
using Android.OS;
using Excalibur.Tests.FormsCross.ViewModels;
using MvvmCross.Forms.Droid.Views;

namespace Excalibur.Tests.FormsCross.Droid
{
    [Activity(
        Label = "AppActivity", 
        Theme = "@style/Theme",
        // MainLauncher = true, // No Splash Screen: Uncomment this lines if removing splash screen
        LaunchMode = LaunchMode.SingleTask)]
    public class AppActivity : MvxFormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabbar;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            // No Splash Screen: Uncomment these lines if removing splash screen
            // var startup = Mvx.Resolve<IMvxAppStart>();
            // startup.Start();
            // InitializeForms(bundle);
        }

        public override void OnBackPressed()
        {
            MoveTaskToBack(false);
        }
    }
}