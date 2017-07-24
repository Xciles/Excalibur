using Android.App;
using Android.Content.PM;
using Android.OS;
using Excalibur.Tests.Cross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace Excalibur.Tests.Cross.Droid.Activities
{
    [Activity(
        Label = "Examples",
        Theme = "@style/Theme.Login",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
    )]
    public class LoginActivity : MvxAppCompatActivity<LoginViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_login);
        }
    }
}
