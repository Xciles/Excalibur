using MvvmCross.Forms.Views.Attributes;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Pages
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
