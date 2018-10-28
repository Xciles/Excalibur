using System.Threading.Tasks;
using Excalibur.Cross.Business;

namespace Excalibur.Tests.Encrypted.Cross.Core.Business.Interfaces
{
    public interface ILoggedInUser : ISingleBusiness<Domain.LoggedInUser>
    {
        void StoreForSaveAfterPin(Domain.LoggedInUser user);
        Task Store();
    }
}