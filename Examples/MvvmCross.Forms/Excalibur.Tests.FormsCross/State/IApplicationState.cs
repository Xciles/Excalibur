using Excalibur.Shared.State;

namespace Excalibur.Tests.FormsCross.State
{
    public interface IApplicationState : IBaseState
    {
        string Email { get; set; }
    }
}
