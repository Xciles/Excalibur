using Excalibur.Base.Configuration;
using Excalibur.Base.State;
using Excalibur.Tests.Encrypted.Cross.Core.Configuration;

namespace Excalibur.Tests.Encrypted.Cross.Core.State
{
    public class ApplicationState : BaseState<Config>, IApplicationState
    {
        public ApplicationState(IConfigurationManager configurationManager) : base(configurationManager)
        {
        }

        public string Email
        {
            get { return Config.Email; }
            set { Config.Email = value; }
        }

        public bool Authenticated
        {
            get { return Config.Authenticated; }
            set { Config.Authenticated = value; }
        }

        public int? PinAttempts
        {
            get { return Config.PinAttempts; }
            set { Config.PinAttempts = value; }
        }

        public string Pin
        {
            get { return Config.Pin; }
            set { Config.Pin = value; }
        }
    }
}
