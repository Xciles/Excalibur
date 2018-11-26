using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;

namespace Excalibur.Tests.FormsCross.Core.Services
{
    public class TodoService : ServiceBase<IList<Domain.Todo>>
    {
        public override async Task<IList<Domain.Todo>> SyncData()
        {
            using (var client = CreateDefaultHttpClient())
            {
                var responseMessage = await client.GetAsync("https://jsonplaceholder.typicode.com/todos");
                var result = responseMessage.ConvertFromJsonResponse<IList<Domain.Todo>>();

                return result.Result;
            }
        }
    }
}
