using Excalibur.Cross.Presentation;
using MvvmCross;
using MvvmCross.Commands;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels.Menu
{
    public class MenuItem
    {
        public string Title { get; set; }

        public string ImageName { get; set; }

        public IMvxCommand Navigate { get; set; }
    }

    public class HeaderMenuItem : MenuItem
    {

    }

    public class CurrentUserMenuItem : MenuItem
    {
        public CurrentUserMenuItem()
        {
            var userPresentation = Mvx.IoCProvider.Resolve<ISinglePresentation<int, Core.Observable.LoggedInUser>>();
            CurrentUserObservable = userPresentation.SelectedObservable;
        }

        public Core.Observable.LoggedInUser CurrentUserObservable { get; set; }
    }
}
