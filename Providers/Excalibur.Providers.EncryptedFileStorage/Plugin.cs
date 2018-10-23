using Excalibur.Base.Configuration;
using Excalibur.Base.Storage;
using MvvmCross;
using MvvmCross.Plugin;

namespace Excalibur.Providers.EncryptedFileStorage
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.RegisterType<IConfigurationManager, EncryptedConfigurationManager>();
            Mvx.IoCProvider.RegisterType<IStorageService, EncryptedStorageService>();
            Mvx.IoCProvider.RegisterType<IEncryptedProviderConfig, EncryptedFileStorageConfig>();
        }
    }
}