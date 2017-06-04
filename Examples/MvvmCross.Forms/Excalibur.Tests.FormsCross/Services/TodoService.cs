using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.FormsCross.Domain;
using Xciles.Uncommon.Net;

namespace Excalibur.Tests.FormsCross.Services
{
    public class TodoService : IServiceBase<IList<Todo>>
    {
        public async Task<IList<Todo>> SyncDataAsync()
        {
            var result = await UncommonRequestHelper.ProcessGetRequestAsync<IList<Todo>>("https://jsonplaceholder.typicode.com/todos");

            return result.Result;
        }
    }
}
