using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class CurrentUserViewModel : DetailViewModel<int, Observable.LoggedInUser, ISinglePresentation<int, Observable.LoggedInUser>>
    {
    }
}
