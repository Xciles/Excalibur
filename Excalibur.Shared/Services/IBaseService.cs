using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Excalibur.Shared.Services
{
    public interface IServiceBase<TDomain>
    {
        Task<TDomain>
    }

    public class ServiceBase : IServiceBase
    {
        
    }
}
