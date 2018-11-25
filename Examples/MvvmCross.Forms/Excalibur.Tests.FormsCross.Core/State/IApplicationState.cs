using Excalibur.Base.State;

namespace Excalibur.Tests.FormsCross.Core.State
{
    public interface IApplicationState : IBaseState
    {
        bool Authenticated { get; set; }
        int UserId { get; set; }
        string Email { get; set; }
        bool HasConfiguration();
    }
}
