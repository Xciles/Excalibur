using Excalibur.Cross.Presentation;
using Excalibur.Cross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core.ViewModels
{
    public class UserDetailViewModel : DetailViewModel<int, Observable.User, IListPresentation<int, Observable.User, Observable.User>>
    {
        public UserDetailViewModel(IListPresentation<int, Observable.User, Observable.User> presentation) : base(presentation)
        {
        }
    }
}
