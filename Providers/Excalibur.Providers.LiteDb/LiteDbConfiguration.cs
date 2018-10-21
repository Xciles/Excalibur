using System;
using Excalibur.Base.Providers;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.LiteDb
{
    /// <summary>
    /// This is used to provide LiteDb with your configuration.
    /// Please call Configure() in the App.RegisterDependencies() to make sure everything is configured correctly.
    /// </summary>
    public class LiteDbConfiguration : IProviderConfiguration<LiteDbConfig>
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
            var fileStore = Mvx.IoCProvider.Resolve<IMvxFileStore>();

            Configuration.ConnectionString = $"Filename={fileStore.NativePath(Configuration.FileName)};{Configuration.Options}";

            Mvx.IoCProvider.RegisterSingleton<IProviderConfiguration<LiteDbConfig>>(this);
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ILiteDbInstance, LiteDbInstance>();
        }
    }
}
