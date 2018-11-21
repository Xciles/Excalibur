using LiteDB;

namespace Excalibur.Providers.LiteDb
{
    /// <summary>
    /// This interface provides sharing of the LiteDb Database
    /// </summary>
    public interface ILiteDbInstance
    {
        /// <summary>
        /// An instance of LiteDbs Database connection
        /// </summary>
        LiteDatabase LiteDatabase { get; }

        /// <summary>
        /// Reinitialize the LiteDb LiteDatabase instance.
        /// </summary>
        void ReinitializeInstance();
    }
}