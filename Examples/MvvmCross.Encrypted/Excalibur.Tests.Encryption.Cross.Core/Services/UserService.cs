using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Encrypted.Cross.Core.Domain;

namespace Excalibur.Tests.Encrypted.Cross.Core.Services
{
    public class UserService : ServiceBase<IList<User>>
    {
        public override async Task<IList<User>> SyncDataAsync()
        {
            using (var client = CreateDefaultHttpClient())
            {
                var responseMessage = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
                var result = responseMessage.ConvertFromJsonResponse<IList<User>>();

                return result.Result;
            }
        }
    }
}
