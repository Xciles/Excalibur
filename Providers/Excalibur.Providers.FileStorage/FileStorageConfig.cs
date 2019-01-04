using Excalibur.Base.Providers;

namespace Excalibur.Providers.FileStorage
{
    /// <summary>
    /// This class provides file storage configuration.
    /// You can set the DataFolder and File Naming format as you like.
    /// </summary>
    public class FileStorageConfig : IProviderConfig
    {
        /// <summary>
        /// Data folder to store files in.
        /// Default is 'data'.
        /// </summary>
        public string DataFolder { get; set; } = "data";

        /// <summary>
        /// Used for naming files.
        /// Please use '{0}' in your string. This is required for naming the files.
        /// </summary>
        public string FileNamingFormat { get; set; } = "{0}.json";
    }
}