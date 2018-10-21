using System.Threading.Tasks;
using Excalibur.Base.Configuration;

namespace Excalibur.Providers.EncryptedFileStorage
{
    /// <summary>
    /// ConfigurationManager that will manage the Configuration, but then encrypted. 
    /// </summary>
    public interface IEncryptedConfigurationManager : IConfigurationManager
    {
        /// <summary>
        /// Loads the configuration using TConfigObject as storage entity
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <param name="password">Password to use</param>
        /// <param name="salt">Salt to use</param>
        /// <returns>An await able Task with the configuration as result</returns>
        Task<TConfigObject> LoadAsync<TConfigObject>(string password, byte[] salt) where TConfigObject : new();
        /// <summary>
        /// Saves the configuration
        /// </summary>
        /// <typeparam name="TConfigObject">The type used for storing the configuration</typeparam>
        /// <param name="configObject">The object that contains the configuration</param>
        /// <param name="password">Password to use</param>
        /// <param name="salt">Salt to use</param>
        /// <returns>An await able Task with the success as result</returns>
        Task<bool> SaveAsync<TConfigObject>(TConfigObject configObject, string password, byte[] salt) where TConfigObject : new();
    }
}