using System.Threading.Tasks;
using Excalibur.Base.Providers;
using Excalibur.Base.State;
using Excalibur.Providers.FileStorage;

namespace Excalibur.Providers.EncryptedFileStorage
{
    ///// <summary>
    ///// Base class for managing state but via the <see cref="EncryptedConfigurationManager"/>
    ///// This class will initialize a <see cref="EncryptedConfigurationManager"/> and will implement certain methods from <see cref="BaseState{TConfig}"/> to contain
    ///// a default implementation.
    ///// </summary>
    ///// <typeparam name="TConfig">A config class that contains the configuration that contains persistable state</typeparam>
    //public abstract class EncryptedBaseState<TConfig> : BaseState<TConfig>, IBaseState
    //    where TConfig : new()
    //{
    //    /// <summary>
    //    /// Initializes the BaseState.
    //    /// </summary>
    //    protected EncryptedBaseState(IEncryptedConfigurationManager configurationManager) : base(configurationManager)
    //    {
    //        ConfigurationManager = configurationManager;
    //    }

    //    /// <inheritdoc />
    //    public virtual async Task InitAndLoadAsync(string password, byte[] salt)
    //    {
    //        Config = await ConfigurationManager.LoadAsync<TConfig>(password, salt).ConfigureAwait(false);

    //        await Initialize().ConfigureAwait(false);
    //    }

    //    /// <inheritdoc />
    //    public virtual async Task SaveAsync(string password, byte[] salt)
    //    {
    //        await ConfigurationManager.SaveAsync(Config, password, salt).ConfigureAwait(false);
    //    }
    //}

    public interface IEncryptedProviderConfig : IProviderConfig
    {
        string ProtectedStoreKeyIdentifier { get; set; }
        string ProtectedStoreSaltIdentifier { get; set; }
    }

    public class EncryptedFileStorageConfig : FileStorageConfig, IEncryptedProviderConfig
    {
        public string ProtectedStoreKeyIdentifier { get; set; } = "ex.ps.key";
        public string ProtectedStoreSaltIdentifier { get; set; } = "ex.ps.salt";
    }
}