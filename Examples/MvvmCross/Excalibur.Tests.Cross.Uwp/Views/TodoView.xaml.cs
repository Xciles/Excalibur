using MvvmCross.Uwp.Attributes;
using MvvmCross.Uwp.Views;

namespace Excalibur.Tests.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[MvxRegionPresentation("PageContent")]
    public sealed partial class TodoView
    {
        public TodoView()
        {
            InitializeComponent();
        }
    }
}
