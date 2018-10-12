using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Cross.Core.Observable;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class CurrentUserViewModel : DetailViewModel<int, Observable.LoggedInUser, ISinglePresentation<int, Observable.LoggedInUser>>
    {
        public CurrentUserViewModel(ISinglePresentation<int, LoggedInUser> presentation) : base(presentation)
        {
        }
    }
}
