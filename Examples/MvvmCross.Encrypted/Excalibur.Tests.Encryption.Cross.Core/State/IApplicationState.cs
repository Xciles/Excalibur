using Excalibur.Base.State;

namespace Excalibur.Tests.Encrypted.Cross.Core.State
{
    public interface IApplicationState : IBaseState
    {
        string Email { get; set; }
        bool Authenticated { get; set; }
        int? PinAttempts { get; set; }
        string Pin { get; set; }
        bool HasConfiguration();
    }
}
