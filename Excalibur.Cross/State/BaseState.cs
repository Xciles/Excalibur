﻿using System.Threading.Tasks;
using Excalibur.Cross.Configuration;

namespace Excalibur.Cross.State
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for managing state. 
    /// This class will initialize a <see cref="P:Excalibur.Cross.State.BaseState`1.ConfigurationManager" /> and will implement certain methods from <see cref="T:Excalibur.Cross.State.IBaseState" /> to contain
    /// a default implementation.
    /// </summary>
    /// <typeparam name="TConfig">A config class that contains the configuration that contains persistable state</typeparam>
    public abstract class BaseState<TConfig> : IBaseState
        where TConfig : new()
    {
        /// <summary>
        /// The configuration manager that manages the Config
        /// </summary>
        protected IConfigurationManager ConfigurationManager { get; set; }

        /// <summary>
        /// Initializes the BaseState.
        /// </summary>
        protected BaseState(IConfigurationManager configurationManager)
        {
            ConfigurationManager = configurationManager;
        }

        /// <summary>
        /// The config that contains persistable state. 
        /// </summary>
        protected TConfig Config { get; set; } = new TConfig();

        /// <inheritdoc />
        public virtual async Task InitAndLoad()
        {
            Config = await ConfigurationManager.Load<TConfig>().ConfigureAwait(false);

            await Initialize().ConfigureAwait(false);
        }

        /// <summary>
        /// Initialize method that will be used for loading default thing if needed.
        /// </summary>
        protected virtual Task Initialize()
        {
            // Add custom things here
            // Like default images
            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public virtual async Task Save() => await ConfigurationManager.Save(Config).ConfigureAwait(false);

        /// <inheritdoc />
        public virtual bool HasConfiguration() => ConfigurationManager.HasConfigurationFor<TConfig>();

        /// <inheritdoc />
        public virtual void Reset() => ConfigurationManager.Reset<TConfig>();
    }
}
