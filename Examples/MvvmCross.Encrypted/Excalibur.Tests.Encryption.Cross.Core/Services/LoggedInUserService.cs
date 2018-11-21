using System;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Encrypted.Cross.Core.Domain;

namespace Excalibur.Tests.Encrypted.Cross.Core.Services
{
    public class LoggedInUserService : ServiceBase<LoggedInUser>
    {
        public override Task<LoggedInUser> SyncData()
        {
            throw new NotImplementedException();
        }
    }
}
