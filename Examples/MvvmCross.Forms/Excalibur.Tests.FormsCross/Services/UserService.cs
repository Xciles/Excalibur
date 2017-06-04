using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.FormsCross.Domain;
using Xciles.Uncommon.Net;

namespace Excalibur.Tests.FormsCross.Services
{
    public class UserService : IServiceBase<IList<User>>
    {
        public async Task<IList<User>> SyncDataAsync()
        {
            var result = await UncommonRequestHelper.ProcessGetRequestAsync<IList<User>>("https://jsonplaceholder.typicode.com/users");

            return result.Result;
        }
    }
}
