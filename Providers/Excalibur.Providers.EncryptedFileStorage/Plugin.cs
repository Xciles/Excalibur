using Excalibur.Base.Configuration;
using Excalibur.Base.Storage;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.Providers.Encryption;
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
            if (Mvx.IoCProvider.CanResolve<IProtectedStore>())
            {
                RegisterDependencies();
            }
            else
            {
                Mvx.IoCProvider.CallbackWhenRegistered<IProtectedStore>(RegisterDependencies);
            }
        }

        private static void RegisterDependencies()
        {
            if (!Mvx.IoCProvider.CanResolve<IExCrypto>())
            {
                Mvx.IoCProvider.ConstructAndRegisterSingleton<IExCrypto, ExCrypto>();
                Mvx.IoCProvider.ConstructAndRegisterSingleton<IEncryptedProviderConfig, EncryptedFileStorageConfig>();
                Mvx.IoCProvider.RegisterType<IStorageService, EncryptedStorageService>();
                Mvx.IoCProvider.RegisterType<IConfigurationManager, ConfigurationManager>();
            }
        }
    }
}