using System.Threading.Tasks;
using Excalibur.Shared.Configuration;
using XLabs.Ioc;

namespace Excalibur.Shared.State
{
    public abstract class BaseState<TConfig> : IBaseState<TConfig>
        where TConfig : new()
    {
        protected IConfigurationManager ConfigurationManager { get; set; }

        protected BaseState()
        {
            ConfigurationManager = Resolver.Resolve<IConfigurationManager>();
        }

        protected TConfig Config { get; set; } = new TConfig();

        public virtual async Task InitAndLoadAsync()
        {
            Config = await ConfigurationManager.LoadAsync<TConfig>().ConfigureAwait(false);

            await Initialize().ConfigureAwait(false);
        }

        protected virtual async Task Initialize()
        {
            // Add custom things here
            // Like default images
        }

        public virtual async Task SaveAsync()
        {
            await ConfigurationManager.SaveAsync(Config).ConfigureAwait(false);
        }
    }
}
