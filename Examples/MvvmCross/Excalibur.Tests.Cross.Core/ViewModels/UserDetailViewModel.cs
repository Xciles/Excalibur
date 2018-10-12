using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;
using Excalibur.Tests.Cross.Core.Observable;

namespace Excalibur.Tests.Cross.Core.ViewModels
{
    public class UserDetailViewModel : DetailViewModel<int, Observable.User, IListPresentation<int, Observable.User, Observable.User>>
    {
        public UserDetailViewModel(IListPresentation<int, User, User> presentation) : base(presentation)
        {
        }
    }
}
