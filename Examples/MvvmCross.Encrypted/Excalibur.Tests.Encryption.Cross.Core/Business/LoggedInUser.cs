using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.Providers;
using Excalibur.Cross.Services;
using Excalibur.Tests.Encrypted.Cross.Core.Business.Interfaces;

namespace Excalibur.Tests.Encrypted.Cross.Core.Business
{
    public class LoggedInUser : BaseSingleBusiness<int, Domain.LoggedInUser>, ILoggedInUser
    {
        private static Domain.LoggedInUser _tempUser;

        public LoggedInUser(IServiceBase<Domain.LoggedInUser> service, IDatabaseProvider<int, Domain.LoggedInUser> storageProvider) : base(service, storageProvider)
        {
        }

        public void StoreForSaveAfterPin(Domain.LoggedInUser user)
        {
            _tempUser = user;
        }

        public async Task Store()
        {
            await StoreItem(_tempUser);

            PublishUpdated(_tempUser);
            _tempUser = null;
        }
    }
}
