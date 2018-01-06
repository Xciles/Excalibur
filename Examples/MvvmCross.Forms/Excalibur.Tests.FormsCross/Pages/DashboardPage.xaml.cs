using MvvmCross.Forms.Views.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, WrapInNavigationPage = true, NoHistory = true)]
    public partial class DashboardPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }
    }
}
