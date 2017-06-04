using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Views;
using MvvmCross.Forms.Uwp.Presenters;
using MvvmCross.Platform;

namespace Excalibur.Tests.FormsCross.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var start = Mvx.Resolve<IMvxAppStart>();
            start.Start();

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsUwpCustomPresenter;
            //var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsUwpMasterDetailPagePresenter;
            LoadApplication(presenter.MvxFormsApp);
        }
    }
}