using System.Threading.Tasks;
using Excalibur.Cross.Business;

namespace Excalibur.Tests.FormsCross.Core.Business.Interfaces
{
    public interface ILoggedInUser : ISingleBusiness<Domain.LoggedInUser>
    {
        Task Store(Domain.LoggedInUser user);
    }
}