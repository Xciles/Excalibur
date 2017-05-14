using System.Threading.Tasks;

namespace Excalibur.Shared.Services
{
    public abstract class ServiceBase<T> : IServiceBase<T>
    {
        public abstract Task<T> SyncDataAsync();
    }
}