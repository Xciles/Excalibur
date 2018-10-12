using MvvmCross.Platforms.Uap.Presenters.Attributes;

namespace Excalibur.Tests.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[MvxRegionPresentation("PageContent")]
    public sealed partial class DashboardView
    {
        public DashboardView()
        {
            this.InitializeComponent();
        }
    }
}
