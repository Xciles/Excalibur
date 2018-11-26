using MvvmCross.Forms.Presenters.Attributes;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Core.Ui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxContentPagePresentation(WrapInNavigationPage = false)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
