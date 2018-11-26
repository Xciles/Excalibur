using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Presentation;

namespace Excalibur.Tests.FormsCross.Core.Presentation
{
    public class LoggedInUser : BaseSinglePresentation<int, Domain.LoggedInUser, Observable.LoggedInUser>
    {
        public LoggedInUser(IObjectMapper<Domain.LoggedInUser, Observable.LoggedInUser> domainSelectedMapper) : base(domainSelectedMapper)
        {
        }
    }
}
