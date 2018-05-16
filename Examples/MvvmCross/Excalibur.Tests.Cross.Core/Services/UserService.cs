using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.Cross.Core.Domain;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class UserService : ServiceBase<IList<User>>
    {
        public override async Task<IList<User>> SyncDataAsync()
        {
            var responseMessage = await SharedClient.GetAsync("https://jsonplaceholder.typicode.com/users");
            var result = responseMessage.ConvertFromJsonResponse<IList<User>>();

            return result.Result;
        }
    }
}
