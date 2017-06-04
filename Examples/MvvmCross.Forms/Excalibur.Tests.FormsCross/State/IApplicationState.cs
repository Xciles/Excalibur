using Excalibur.Shared.State;
using Excalibur.Tests.FormsCross.Configuration;

namespace Excalibur.Tests.FormsCross.State
{
    public interface IApplicationState : IBaseState<Config>
    {
        string Email { get; set; }
    }
}
