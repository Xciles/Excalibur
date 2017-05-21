using Excalibur.Shared.Business;
using MvvmCross.Core.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class FirstViewModel 
        : MvxViewModel
    {
        public FirstViewModel()
        {
            Resolver.Resolve<IListBusiness<int, Domain.User>>().PublishFromStorageAsync();
        }

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
