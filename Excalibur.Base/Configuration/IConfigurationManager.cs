using System.Threading.Tasks;

namespace Excalibur.Base.Configuration
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
        /// <returns>An await able Task with the configuration as result</returns>
        Task<TConfigObject> Load<TConfigObject>() where TConfigObject : new();

        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <param name="configObject">The object that contains the configuration</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> Save<TConfigObject>(TConfigObject configObject) where TConfigObject : new();

        /// <summary>
        /// Returns if the configuration manager is currently managing some kind of configuration for config object
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <returns>An await able Task with the success as result</returns>
        bool HasConfigurationFor<TConfigObject>() where TConfigObject : new();
    }
}
