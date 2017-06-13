using Excalibur.Tests.Cross.Uwp.Utils;
using Excalibur.Tests.Cross.Uwp.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Uwp.Views;

namespace Excalibur.Tests.Cross.Uwp.Controls
{
    public class CustomViewPresenter : MvxExWindowsMultiRegionViewPresenter
    {
        private readonly IMvxWindowsFrame _rootFrame;

        public CustomViewPresenter(IMvxWindowsFrame rootFrame)
            : base(rootFrame)
        {
            _rootFrame = rootFrame;
        }

        public override void ChangePresentation(MvxPresentationHint hint)
        {
            if (hint is MvxPanelPopToRootPresentationHint)
            {
                var mainView = _rootFrame.Content as MainView;
                if (mainView != null)
                {
                    mainView.PopToRoot();
                }
            }

            base.ChangePresentation(hint);
        }
    }
}
