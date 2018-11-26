using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;

namespace Excalibur.Tests.FormsCross.Core.Services
{
    public class UserService : ServiceBase<IList<Domain.User>>
    {
        public override async Task<IList<Domain.User>> SyncData()
        {
            using (var client = CreateDefaultHttpClient())
            {
                var responseMessage = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
                var result = responseMessage.ConvertFromJsonResponse<IList<Domain.User>>();

                return result.Result;
            }
        }
    }
}
