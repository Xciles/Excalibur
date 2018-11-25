using Excalibur.Tests.FormsCross.Core.ViewModels;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms.Xaml;

namespace Excalibur.Tests.FormsCross.Core.Ui.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Master, WrapInNavigationPage = false, NoHistory = true)]
    public partial class MenuPage : MvxContentPage<MenuViewModel>
    {
        public MenuPage()
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
                //if (e.PropertyName == nameof(ViewModel.SelectedMenu))
                //{
                //    if (Parent is MasterDetailPage master)
                //    {
                //        //master.MasterBehavior = MasterBehavior.Popover;
                //        master.IsPresented = !master.IsPresented;
                //    }
                //}
            };
        }
    }
}