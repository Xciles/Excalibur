using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.Cross.Core.Domain;
using Xciles.Uncommon.Net;

namespace Excalibur.Tests.Cross.Core.Services
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
