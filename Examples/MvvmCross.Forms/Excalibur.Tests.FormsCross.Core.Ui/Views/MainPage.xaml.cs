using Excalibur.Tests.FormsCross.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Core.Ui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(MasterDetailPosition.Root, WrapInNavigationPage = false)]
    public partial class MainPage : MvxMasterDetailPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}
