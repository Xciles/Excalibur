using Excalibur.Shared.State;
using Excalibur.Tests.Cross.Core.Configuration;

namespace Excalibur.Tests.Cross.Core.State
{
    public class ApplicationState : BaseState<Config>, IApplicationState
    {
        public string Email
        {
            get { return Config.Email; }
            set { Config.Email = value; }
        }
    }
}
