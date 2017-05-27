using Excalibur.Shared.State;
using Excalibur.Tests.Cross.Core.Configuration;

namespace Excalibur.Tests.Cross.Core.State
{
    public interface IApplicationState : IBaseState<Config>
    {
        string Email { get; set; }
    }
}
