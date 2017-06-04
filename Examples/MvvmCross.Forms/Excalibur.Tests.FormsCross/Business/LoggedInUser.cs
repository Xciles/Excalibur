using System.Threading.Tasks;
using Excalibur.Shared.Business;
using Excalibur.Tests.FormsCross.Business.Interfaces;

namespace Excalibur.Tests.FormsCross.Business
{
    public class LoggedInUser : BaseSingleBusiness<int, Domain.LoggedInUser>, ILoggedInUser
    {
        public async Task Store(Domain.LoggedInUser user)
        {
            await StoreItemAsync(user).ConfigureAwait(false);

            PublishUpdated(user);
        }
    }
}
