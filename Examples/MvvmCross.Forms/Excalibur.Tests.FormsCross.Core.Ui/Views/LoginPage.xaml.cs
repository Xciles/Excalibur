using Excalibur.Tests.FormsCross.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;

namespace Excalibur.Tests.FormsCross.Core.Ui.Views
{
    [MvxContentPagePresentation(WrapInNavigationPage = false)]
    public partial class LoginPage : MvxContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}