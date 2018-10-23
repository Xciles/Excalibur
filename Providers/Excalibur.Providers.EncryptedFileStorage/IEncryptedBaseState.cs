//using System.Threading.Tasks;
//using Excalibur.Base.State;

//namespace Excalibur.Providers.EncryptedFileStorage
//{
//    /// <summary>
//    /// Interface for managing encrypted state of an application.
//    /// </summary>
//    public interface IEncryptedBaseState : IBaseState
//    {
//        /// <summary>
//        /// Initialize and load the state
//        /// </summary>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns>An await-able task</returns>
//        Task InitAndLoadAsync(string password, byte[] salt);
//        /// <summary>
//        /// Save the state.
//        /// </summary>
//        /// <param name="password">Password to use</param>
//        /// <param name="salt">Salt to use</param>
//        /// <returns>An await-able task</returns>
//        Task SaveAsync(string password, byte[] salt);
//    }
//}