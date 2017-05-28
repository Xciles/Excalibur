using System.Threading.Tasks;
using Excalibur.Shared.Business;

namespace Excalibur.Tests.Cross.Core.Business.Interfaces
{
    public interface ILoggedInUser : ISingleBusiness<Domain.LoggedInUser>
    {
        Task Store(Domain.LoggedInUser user);
    }
}