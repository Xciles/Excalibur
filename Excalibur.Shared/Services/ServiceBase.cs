using System.Collections.Generic;
using System.Threading.Tasks;
using Excalibur.Shared.Storage;

namespace Excalibur.Shared.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T>
    {
        public abstract Task<T> SyncDataAsync();
    }
}