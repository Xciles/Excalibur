using System;
using Excalibur.Base.Providers;

namespace Excalibur.Providers.LiteDb
{
    public class LiteDbConfiguration : IProviderConfiguration
    {
        public LiteDbConfig Configuration { get; private set; }

        public void Configure(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is LiteDbConfig liteConfig)) throw new ArgumentException("Please provide LiteDbConfig instance", nameof(config));

            Configuration = liteConfig;
        }
    }

    public class LiteDbConfig : IProviderConfig
    {
        public string ConnectionString { get; set; }
    }

    public class LiteDbProvider
    {
        // Insert
        // BulkInsert
        // Update
        // Upsert
        // Delete

        // Find
        // FindOne

        // EnsureIndex

        // Provider > LiteProvider > LiteImplementation
        // Makes sure the the base provider can be used as well as custom providers per implementation (and in businessess/presentation w/e)
    }
}
