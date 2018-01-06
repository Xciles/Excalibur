using MvvmCross.Forms.Views.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(WrapInNavigationPage = true, NoHistory = false)]
    public partial class TodoDetailPage
    {
        public TodoDetailPage()
        {
            InitializeComponent();
        }
    }
}
