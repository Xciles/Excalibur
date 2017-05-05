using System.Collections.Generic;
using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    public interface IBusiness
    {
        Task UpdateFromServiceAsync();
    }

    public interface ISingleBusiness<TDomain> : IBusiness
    {
        Task<TDomain> GetAsync();
        Task DeleteAsync();
    }

    public interface IListBusiness<in TId, TDomain> : IBusiness
    {
        Task<IList<TDomain>> GetAllAsync();
        Task<TDomain> GetByIdAsync(TId id);
        Task DeleteItemAsync(TId id);
    }
}
