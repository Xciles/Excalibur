using System;
using Excalibur.Base.Providers;
using LiteDB;

namespace Excalibur.Providers.LiteDb
{
    /// <inheritdoc cref="ILiteDbInstance"/>
    public class LiteDbInstance : ILiteDbInstance, IDisposable
    {
        private readonly LiteDbConfig _providerConfig;

        /// <inheritdoc cref="ILiteDbInstance"/>
        public LiteDatabase LiteDatabase { get; private set; }

        public LiteDbInstance(IProviderConfiguration<LiteDbConfig> providerConfiguration)
        {
            _providerConfig = providerConfiguration.Configuration;
            ReinitializeInstance();
        }

        /// <inheritdoc />
        public void ReinitializeInstance()
        {
            LiteDatabase?.Dispose();
            LiteDatabase = new LiteDatabase(_providerConfig.ConnectionString);
        }

        ~LiteDbInstance()
        {
            Dispose(false);
        }

        private void ReleaseUnmanagedResources()
        {
            LiteDatabase.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                LiteDatabase?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}