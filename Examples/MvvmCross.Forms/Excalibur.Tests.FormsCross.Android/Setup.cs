using Android.Content;
using MvvmCross.Platform;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Platform;

namespace Excalibur.Tests.FormsCross.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Excalibur.Tests.FormsCross.App();
        }

        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxFormsDroidCustomPresenter();
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }
    }

    public class MvxFormsDroidCustomPresenter : CustomPresenter, IMvxAndroidViewPresenter
    {
        public MvxFormsDroidCustomPresenter()
        {
        }

        public MvxFormsDroidCustomPresenter(MvxFormsApplication mvxFormsApp)
            : base(mvxFormsApp)
        {
        }
    }
}