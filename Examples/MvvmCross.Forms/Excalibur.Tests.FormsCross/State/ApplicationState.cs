using Excalibur.Shared.State;
using Excalibur.Tests.FormsCross.Configuration;

namespace Excalibur.Tests.FormsCross.State
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
