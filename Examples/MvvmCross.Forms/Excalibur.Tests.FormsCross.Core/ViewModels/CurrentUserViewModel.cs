using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class CurrentUserViewModel : DetailViewModel<int, Observable.LoggedInUser, ISinglePresentation<int, Observable.LoggedInUser>>
    {
        public CurrentUserViewModel(ISinglePresentation<int, Observable.LoggedInUser> presentation) : base(presentation)
        {
        }
    }
}
