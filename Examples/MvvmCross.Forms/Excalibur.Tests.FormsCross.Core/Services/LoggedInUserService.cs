using System;
using System.Threading.Tasks;
using Excalibur.Cross.Services;

namespace Excalibur.Tests.FormsCross.Core.Services
{
    public class LoggedInUserService : ServiceBase<Domain.LoggedInUser>
    {
        public override Task<Domain.LoggedInUser> SyncData()
        {
            throw new NotImplementedException();
        }
    }
}
