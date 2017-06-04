using Excalibur.Tests.Cross.Core.ViewModels;
using Foundation;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Support.XamarinSidebar.Attributes;

namespace Excalibur.Tests.Cross.iOS.Views
{
    [Register("MenuView")]
    [MvxSidebarPresentation(MvxPanelEnum.Left, MvxPanelHintType.PushPanel, false)]
    public partial class MenuView : BaseViewController<MainViewModel>
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}
