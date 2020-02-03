using Excalibur.Cross.Providers;
using MvvmCross;
using MvvmCross.Plugin.File;

namespace Excalibur.Providers.LiteDb
{
    /// <summary>
    /// This class provides LiteDb configuration.
    /// LiteDb lets you set various settings in the connection string as you can see in this link
    ///  https://github.com/mbdavid/LiteDB/wiki/Connection-String
    /// FileName represents the file name (and path if needed) of a given store. You don't have to provide 'FileName='.
    /// Options provide you with the ability to set additional settings.
    /// </summary>
    public class LiteDbConfig : IProviderConfig
    {
        /// <summary>
        /// The file name, including path, of a given store. You don't have to provide 'FileName='.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Password option for LiteDb
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Other (aside from filename and password) configurable LiteDb options should be inputted here.
        /// </summary>
        public string Options { get; set; }

        /// <summary>
        /// Processed connection string. Concatenating FileName and Options, if options are present.
        /// </summary>
        internal string ConnectionString
        {
            get
            {
                var fileStore = Mvx.IoCProvider.Resolve<IMvxFileStore>();

                var result = $"Filename={fileStore.NativePath(FileName)};";
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    result += $"Password={Password};";
                }

                if (!string.IsNullOrWhiteSpace(Password))
                {
                    result += Options;
                }

                return result;
            }
        }
    }
}