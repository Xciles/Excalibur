using Excalibur.Base.Configuration;
using Excalibur.Base.Storage;
using Excalibur.Providers.FileStorage;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;

namespace Excalibur.Providers.EncryptedFileStorage
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IEncryptedProviderConfig, EncryptedFileStorageConfig>();
            Mvx.IoCProvider.RegisterType<IStorageService, EncryptedStorageService>();
            Mvx.IoCProvider.RegisterType<IConfigurationManager, ConfigurationManager>();
        }
    }
}