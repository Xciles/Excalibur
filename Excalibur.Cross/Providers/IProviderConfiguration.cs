namespace Excalibur.Cross.Providers
{
    /// <summary>
    /// Interface for configuring providers in the same way
    /// </summary>
    /// <typeparam name="T">An implementation of the IProviderConfig</typeparam>
    public interface IProviderConfiguration<out T>
        where T : IProviderConfig
    {
        /// <summary>
        /// The provider configuration
        /// </summary>
        T Configuration { get; }

        /// <summary>
        /// Method that should provide the actual configuring of a provider
        /// </summary>
        void Configure();
    }
}
