using System.Threading.Tasks;

namespace Excalibur.Shared.Services
{
    public interface IServiceBase<T>
    {
        Task<T> SyncDataAsync();
    }
}
