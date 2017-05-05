using System.Threading.Tasks;
using Excalibur.Shared.Storage;
using System.Collections.Generic;

namespace Excalibur.Shared.Services
{
    public interface IServiceBase<T>
    {
        Task<T> SyncDataAsync();
    }
}
