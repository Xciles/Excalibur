using MvvmCross.Core.ViewModels;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }

        public MvxCommand GoMyTestViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MyTestViewModel>()); }
        }

        public MvxCommand GoUserViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<UserViewModel>()); }
        }
    }
}
