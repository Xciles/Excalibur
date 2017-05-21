using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    public interface IBusiness
    {
        Task UpdateFromServiceAsync();
        Task PublishFromStorageAsync();
    }
}
