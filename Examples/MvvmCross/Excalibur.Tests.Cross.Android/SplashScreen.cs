using Android.App;
using Android.Content.PM;
using Android.OS;
using Excalibur.Tests.Cross.Core;
using MvvmCross.Platforms.Android.Views;

namespace Excalibur.Tests.Cross.Droid
{
    [Activity(Label = "Excalibur.Droid",
                MainLauncher = true,
                Icon = "@drawable/icon",
                Theme = "@style/Theme.Splash",
                NoHistory = true,
                ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity<Setup, App>
    {
        public SplashScreen() : base(Resource.Layout.SplashScreen)
        {
        }

        protected override void RunAppStart(Bundle bundle)
        {
            base.RunAppStart(bundle);
        }
    }
}
