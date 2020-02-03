using Excalibur.Cross.Configuration;
using Excalibur.Cross.State;
using Excalibur.Tests.FormsCross.Core.Configuration;

namespace Excalibur.Tests.FormsCross.Core.State
{
    public class ApplicationState : BaseState<Config>, IApplicationState
    {
        public ApplicationState(IConfigurationManager configurationManager) : base(configurationManager)
        {
        }

        public bool Authenticated
        {
            get { return Config.Authenticated; }
            set { Config.Authenticated = value; }
        }

        public int UserId
        {
            get { return Config.UserId; }
            set { Config.UserId = value; }
        }

        public string Email
        {
            get { return Config.Email; }
            set { Config.Email = value; }
        }

        public bool HasConfiguration()
        {
            return ConfigurationManager.HasConfigurationFor<Config>();
        }
    }
}
