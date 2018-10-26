using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Encrypted.Cross.Core.Observable;

namespace Excalibur.Tests.Encrypted.Cross.Core.ViewModels
{
    public class CurrentUserViewModel : DetailViewModel<int, Observable.LoggedInUser, ISinglePresentation<int, Observable.LoggedInUser>>
    {
        public CurrentUserViewModel(ISinglePresentation<int, LoggedInUser> presentation) : base(presentation)
        {
        }
    }
}
