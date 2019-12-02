using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;

namespace Excalibur.AspNetCore.Business.Interfaces
{
    public interface IBaseDataTablesInterface
    {
        Task<DataTablesResponse> IndexDt(IDataTablesRequest request);
    }
}
