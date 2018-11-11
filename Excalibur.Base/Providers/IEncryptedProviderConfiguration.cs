namespace Excalibur.Base.Providers
{
    /// <summary>
    /// Interface for configuring providers in the same way
    /// </summary>
    public interface IEncryptedProviderConfiguration
    {
        /// <summary>
        /// Method that will provide a key to the provider to for example configure encryption keys.
        /// </summary>
        void ConfigureKey(string key);
    }
}