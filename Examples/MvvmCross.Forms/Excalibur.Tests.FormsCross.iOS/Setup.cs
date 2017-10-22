using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Forms.Platform;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;
using UIKit;
using Xamarin.Forms;

namespace Excalibur.Tests.FormsCross.iOS
{
    public class Setup : MvxIosSetup
    {
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Excalibur.Tests.FormsCross.App();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            Forms.Init();

            var xamarinFormsApp = new MvxFormsApplication();

            return new MvxFormsIosCustomPresenter(Window, xamarinFormsApp);
        }
    }
    
    public class MvxFormsIosCustomPresenter : CustomPresenter, IMvxIosViewPresenter
    {
        private readonly UIWindow _window;

        public MvxFormsIosCustomPresenter()
        {
        }

        public MvxFormsIosCustomPresenter(UIWindow window, MvxFormsApplication mvxFormsApp)
            : base(mvxFormsApp)
        {
            _window = window;
        }

        public bool PresentModalViewController(UIViewController controller, bool animated)
        {
            return false;
        }

        public void NativeModalViewControllerDisappearedOnItsOwn()
        {
        }

        protected override void CustomPlatformInitialization(NavigationPage mainPage)
        {
            _window.RootViewController = mainPage.CreateViewController();
        }

        protected override void CustomPlatformInitialization(MasterDetailPage mainPage)
        {
            _window.RootViewController = mainPage.CreateViewController();
        }
    }
}