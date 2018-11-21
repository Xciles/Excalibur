using System.Threading.Tasks;

namespace Excalibur.Base.State
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
    }
}