using System;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Cross.Core.Domain;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class LoggedInUserService : ServiceBase<LoggedInUser>
    {
        public override Task<LoggedInUser> SyncDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
