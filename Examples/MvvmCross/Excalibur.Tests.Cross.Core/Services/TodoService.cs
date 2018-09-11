using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Excalibur.Cross.Services;
using Excalibur.Tests.Cross.Core.Domain;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class TodoService : ServiceBase<IList<Todo>>
    {
        public override async Task<IList<Todo>> SyncDataAsync()
        {
            var responseMessage = await SharedClient.GetAsync("https://jsonplaceholder.typicode.com/todos");
            var result = responseMessage.ConvertFromJsonResponse<IList<Todo>>();

            return result.Result;
        }
    }
}
