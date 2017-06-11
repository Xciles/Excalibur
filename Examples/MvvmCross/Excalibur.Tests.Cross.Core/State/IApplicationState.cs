using Excalibur.Shared.State;

namespace Excalibur.Tests.Cross.Core.State
{
    public interface IApplicationState : IBaseState
    {
        string Email { get; set; }
    }
}
