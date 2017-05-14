using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    public interface IListBusiness<in TId, TDomain> : IBusiness
    {
        Task<IList<TDomain>> GetAllAsync();
        Task<TDomain> GetByIdAsync(TId id);
        Task DeleteItemAsync(TId id);
    }
}