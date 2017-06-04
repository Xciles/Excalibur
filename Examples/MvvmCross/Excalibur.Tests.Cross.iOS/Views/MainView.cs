using Excalibur.Tests.Cross.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Support.XamarinSidebar;
using MvvmCross.iOS.Support.XamarinSidebar.Attributes;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;

namespace Excalibur.Tests.Cross.iOS.Views
{
    // todo add more XamarinSiderbar
    [MvxSidebarPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true, MvxSplitViewBehaviour.Master)]
    public partial class MainView : BaseViewController<MainViewModel>
    {
        public MainView() : base("MainView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewModel.ShowMenu();
        }
    }
}
