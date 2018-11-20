using System.Net.Http;
using System.Threading.Tasks;

namespace Excalibur.Cross.Services
{
    /// <summary>
    /// Base class for services.
    ///
    /// This class provides a static HttpClient as <see cref="SharedClient"/>.
    /// </summary>
    public abstract class ServiceBase
    {
        protected HttpClient CreateDefaultHttpClient()
        {
            return new HttpClient(new AutomaticDecompressionHandler());
        }
    }

    /// <summary>
    /// Abstract base class for services. 
    /// This might be extended with custom checks and method that might be useful for sharing.
    /// </summary>
    /// <typeparam name="T">The type of the object that is used for communication</typeparam>
    public abstract class ServiceBase<T> : ServiceBase, IServiceBase<T>
    {
        /// <summary>
        /// Base method for syncing data.
        /// </summary>
        /// <returns>An await able Task with the resulting objects as result</returns>
        public abstract Task<T> SyncDataAsync();
    }
}