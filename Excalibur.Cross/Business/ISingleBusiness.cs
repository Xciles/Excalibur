using System.Threading.Tasks;

namespace Excalibur.Cross.Business
{
    /// <summary>
    /// An entity to represent a single instance and single domain object. 
    /// This should be used when for example managing a 1 object instead of a list of objects. 
    /// For example, about information, app info, etc.
    /// </summary>
    /// <typeparam name="TDomain">The type of the object that wants to be represented</typeparam>
    public interface ISingleBusiness<TDomain> : IBusiness
    {
        /// <summary>
        /// Get the first available stored object that this business entity manages.
        /// Since a single business only manages one item, this will return that item.
        /// </summary>
        /// <returns>The requested object as result</returns>
        Task<TDomain> FirstOrDefault();

        /// <summary>
        /// Deletes the represented object
        /// </summary>
        /// <returns>An awaitable task</returns>
        Task DeleteAsync();
    }
}