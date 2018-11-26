using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Core.Ui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class UserPage
    {
        public UserPage()
        {
            InitializeComponent();
        }
    }
}
