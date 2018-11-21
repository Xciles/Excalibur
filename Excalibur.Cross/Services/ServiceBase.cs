using System.Net.Http;
using System.Threading.Tasks;

namespace Excalibur.Cross.Services
{
    /// <summary>
    /// Base class for services.
    ///
    /// This class provides method to create a default <see cref="HttpClient"/> with the <see cref="AutomaticDecompressionHandler"/>.
    /// With a static shared client you can run into problems on Android and iOS with requests never returning, timing out etc. Therefore
    ///     we no longer expose a static client, but a create default method, which you have to dispose yourself.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Creates an instance of the <see cref="HttpClient"/> with the <see cref="AutomaticDecompressionHandler"/>
        /// With a static shared client you can run into problems on Android and iOS with requests never returning, timing out etc. Therefore
        ///     we no longer expose a static client, but a create default method, which you have to dispose yourself.
        /// </summary>
        /// <returns>An instance of the HttpClient</returns>
        protected HttpClient CreateDefaultHttpClient() => new HttpClient(new AutomaticDecompressionHandler());
    }

    /// <summary>
    /// Abstract base class for services. 
    /// This might be extended with custom checks and method that might be useful for sharing.
    /// </summary>
    /// <typeparam name="T">The type of the object that is used for communication</typeparam>
    public abstract class ServiceBase<T> : ServiceBase, IServiceBase<T>
    {
        /// <inheritdoc />
        public abstract Task<T> SyncData();
    }
}