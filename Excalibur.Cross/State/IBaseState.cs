using System.Threading.Tasks;

namespace Excalibur.Cross.State
{
    /// <summary>
    /// Interface for managing state of an application.
    /// </summary>
    public interface IBaseState
    {
        /// <summary>
        /// Initialize and load the state
        /// </summary>
        Task InitAndLoad();

        /// <summary>
        /// Save the state.
        /// </summary>
        Task Save();

        /// <summary>
        /// Indicates if the state has a configuration via the ConfigurationManager
        /// </summary>
        bool HasConfiguration();

        /// <summary>
        /// Resets the configuration
        /// </summary>
        void Reset();
    }
}