using System.Threading.Tasks;

namespace Excalibur.Shared.Business
{
    public interface ISingleBusiness<TDomain> : IBusiness
    {
        Task<TDomain> GetAsync();
        Task DeleteAsync();
    }
}