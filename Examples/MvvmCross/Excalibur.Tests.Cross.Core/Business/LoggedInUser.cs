using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Base.Storage;
using Excalibur.Cross.Business;
using Excalibur.Cross.Services;
using Excalibur.Tests.Cross.Core.Business.Interfaces;

namespace Excalibur.Tests.Cross.Core.Business
{
    public class LoggedInUser : BaseSingleBusiness<int, Domain.LoggedInUser>, ILoggedInUser
    {
        public LoggedInUser(IServiceBase<Domain.LoggedInUser> service, IDatabaseProvider<int, Domain.LoggedInUser> storageProvider) : base(service, storageProvider)
        {
        }

        public async Task Store(Domain.LoggedInUser user)
        {
            await StoreItem(user);

            PublishUpdated(user);
        }
    }
}
