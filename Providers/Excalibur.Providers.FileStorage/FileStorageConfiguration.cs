﻿using System;
using Excalibur.Cross.Providers;
using MvvmCross;

namespace Excalibur.Providers.FileStorage
{
    /// <summary>
    /// This is used to provide FileStorage with your configuration.
    /// Please call Configure() in the App.RegisterDependencies() to make sure everything is configured correctly.
    /// </summary>
    public class FileStorageConfiguration : IProviderConfiguration<FileStorageConfig>
    {
        /// <inheritdoc />
        public FileStorageConfig Configuration { get; private set; }

        public FileStorageConfiguration(IProviderConfig config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (!(config is FileStorageConfig liteConfig)) throw new ArgumentException("Please provide FileStorageConfig instance", nameof(config));

            Configuration = liteConfig;
        }

        /// <inheritdoc />
        public void Configure()
        {
            Mvx.IoCProvider.RegisterSingleton<IProviderConfiguration<FileStorageConfig>>(this);
        }
    }
}