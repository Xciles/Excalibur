using System;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.FormsCross.Domain;

namespace Excalibur.Tests.FormsCross.Services
{
    public class LoggedInUserService : IServiceBase<LoggedInUser>
    {
        public Task<LoggedInUser> SyncDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
