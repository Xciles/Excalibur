using MvvmCross.Core.Navigation;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Excalibur.Tests.FormsCross.ViewModels
{
    public class FirstViewModel : MvxViewModel
    {
        protected IMvxNavigationService NavigationService { get; } = Mvx.Resolve<IMvxNavigationService>();
        private IMvxAsyncCommand _goToDetailCommand;


        private string _yourNickname = string.Empty;
        public string YourNickname
        {
            get { return _yourNickname; }
            set { _yourNickname = value; RaisePropertyChanged(() => YourNickname); RaisePropertyChanged(() => Hello); }
        }

        public string Hello
        {
            get { return "Hello " + YourNickname; }
        }


        public IMvxAsyncCommand ShowAboutPageCommand
        {
            get
            {
                _goToDetailCommand = _goToDetailCommand ?? new MvxAsyncCommand(() =>
                {
                    return NavigationService.Navigate<AboutViewModel>();
                });

                return _goToDetailCommand;
            }
        }
    }
}
