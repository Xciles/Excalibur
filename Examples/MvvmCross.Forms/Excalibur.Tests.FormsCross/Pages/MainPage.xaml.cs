using Excalibur.Tests.FormsCross.ViewModels;
using MvvmCross.Forms.Views.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, NoHistory = true)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
#if __IOS__
            if(Parent is MasterDetailPage master)
                master.IsGestureEnabled = false;
#endif
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            ViewModel.PropertyChanged += (sender, e) => {
                if (e.PropertyName == nameof(ViewModel.SelectedMenu))
                {
                    if (Parent is MasterDetailPage master)
                    {
                        //master.MasterBehavior = MasterBehavior.Popover;
                        master.IsPresented = !master.IsPresented;
                    }
                }
            };
        }
    }
}
