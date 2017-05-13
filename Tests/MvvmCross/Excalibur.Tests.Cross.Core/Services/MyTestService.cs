using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.Cross.Core.Domain;

namespace Excalibur.Tests.Cross.Core.Services
{
    public class MyTestService : IServiceBase<IList<MyTestDomain>>
    {
        public async Task<IList<MyTestDomain>> SyncDataAsync()
        {
            return await Task.FromResult(new List<MyTestDomain>());
        }
    }
}
