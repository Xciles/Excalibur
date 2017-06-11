using System;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.FormsCross.Domain;

namespace Excalibur.Tests.FormsCross.Services
{
    public class LoggedInUserService : ServiceBase<LoggedInUser>
    {
        public override Task<LoggedInUser> SyncDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
