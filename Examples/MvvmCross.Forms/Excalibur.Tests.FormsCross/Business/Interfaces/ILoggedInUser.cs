using System.Threading.Tasks;
using Excalibur.Shared.Business;

namespace Excalibur.Tests.FormsCross.Business.Interfaces
{
    public interface ILoggedInUser : ISingleBusiness<Domain.LoggedInUser>
    {
        Task Store(Domain.LoggedInUser user);
    }
}