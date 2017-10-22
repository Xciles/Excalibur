using Android.App;
using Android.Content.PM;
using Android.OS;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Droid.Views;
using MvvmCross.Forms.Platform;
using MvvmCross.Platform;
using Xamarin.Forms;

namespace Excalibur.Tests.FormsCross.Droid
{
    [Activity(Label = "AppActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class AppActivity : MvxFormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            var mvxFormsApp = new MvxFormsApplication();
            LoadApplication(mvxFormsApp);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsDroidCustomPresenter;
            presenter.MvxFormsApp = mvxFormsApp;

            Mvx.Resolve<IMvxAppStart>().Start();
        }
    }
}