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
            return await Task.FromResult(new List<MyTestDomain>
            {
                new MyTestDomain{ Id = 1, Description = "Test item 1", Name = "Test 1" },
                new MyTestDomain{ Id = 2, Description = "Test item 2", Name = "Test 2" },
            });
        }
    }
}
