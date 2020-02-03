using System;
using Excalibur.Cross.Providers;
using MvvmCross;
using MvvmCross.IoC;

namespace Excalibur.Providers.LiteDb
{
    /// <summary>
    /// This is used to provide LiteDb with your configuration.
    /// Please call Configure() in the App.RegisterDependencies() to make sure everything is configured correctly.
    /// </summary>
    public class LiteDbConfiguration : IProviderConfiguration<LiteDbConfig>, IEncryptedProviderConfiguration
    {
        /// <inheritdoc />
        public LiteDbConfig Configuration { get; private set; }

        public LiteDbConfiguration(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is LiteDbConfig liteConfig)) throw new ArgumentException("Please provide LiteDbConfig instance", nameof(config));

            Configuration = liteConfig;
        }

        /// <inheritdoc />
        public void Configure()
        {
            Mvx.IoCProvider.RegisterSingleton<IProviderConfiguration<LiteDbConfig>>(this);
            Mvx.IoCProvider.RegisterSingleton<IEncryptedProviderConfiguration>(this);
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ILiteDbInstance, LiteDbInstance>();
        }

        /// <inheritdoc />
        public void ConfigureKey(string key)
        {
            Configuration.Password = key;

            Mvx.IoCProvider.Resolve<ILiteDbInstance>().ReinitializeInstance();
        }
    }
}
