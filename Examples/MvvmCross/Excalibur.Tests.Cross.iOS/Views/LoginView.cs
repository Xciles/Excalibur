using Excalibur.Tests.Cross.Core.ViewModels;
using Foundation;
using MvvmCross.iOS.Support.XamarinSidebar;

namespace Excalibur.Tests.Cross.iOS.Views
{
    [Register("LoginView")]
    [MvxSidebarPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public partial class LoginView : BaseViewController<MainViewModel>
    {
        public LoginView() : base("LoginView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
        }
    }
}
