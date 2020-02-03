﻿using System.Threading.Tasks;

namespace Excalibur.Cross.Configuration
{
    /// <summary>
    /// ConfigurationManager that will manage the Configuration. 
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Loads the configuration using TConfigObject as storage entity
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <returns>The stored configuration</returns>
        Task<TConfigObject> Load<TConfigObject>() where TConfigObject : new();

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <param name="configObject">The object that contains the configuration</param>
        /// <returns>ATrue if stored successfully, false otherwise.</returns>
        Task<bool> Save<TConfigObject>(TConfigObject configObject) where TConfigObject : new();

        /// <summary>
        /// Resets the configuration which should delete the current configuration
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        void Reset<TConfigObject>() where TConfigObject : new();

        /// <summary>
        /// Returns if the configuration manager is currently managing some kind of configuration for config object
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <returns>True if managing a configuration or if a configuration is available, false otherwise.</returns>
        bool HasConfigurationFor<TConfigObject>() where TConfigObject : new();
    }
}
