﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Services;
using Excalibur.Tests.Cross.Core.Domain;
using Xciles.Uncommon.Net;

namespace Excalibur.Tests.Cross.Core.Services
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