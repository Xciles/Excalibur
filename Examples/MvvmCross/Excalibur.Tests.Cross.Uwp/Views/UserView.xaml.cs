using MvvmCross.Uwp.Views;

namespace Excalibur.Tests.Cross.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
	[MvxRegion("PageContent")]
    public sealed partial class UserView
    {
        public UserView()
        {
            InitializeComponent();
        }
    }
}
