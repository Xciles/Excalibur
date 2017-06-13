using Windows.UI.Xaml.Controls;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Uwp.Views;

namespace Excalibur.Tests.Cross.Uwp.Utils
{
    public class MvxExWindowsMultiRegionViewPresenter : MvxWindowsMultiRegionViewPresenter
    {
        public MvxExWindowsMultiRegionViewPresenter(IMvxWindowsFrame rootFrame) : base(rootFrame)
        {
        }

        public override void Show(MvxViewModelRequest request)
        {
            var viewType = GetViewType(request);

            if (viewType.HasRegionAttribute())
            {
                var requestTranslator = Mvx.Resolve<IMvxWindowsViewModelRequestTranslator>();
                var requestText = requestTranslator.GetRequestTextFor(request);

                var containerView = FindChild<Frame>(_rootFrame.UnderlyingControl, viewType.GetRegionName());

                if (containerView != null)
                {
                    containerView.Navigate(viewType, requestText);
                    return;
                }
            }

            base.Show(request);
        }
    }
}
