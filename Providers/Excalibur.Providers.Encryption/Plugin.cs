using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.Plugin;

namespace Excalibur.Providers.Encryption
{
    [MvxPlugin]
    [Preserve(AllMembers = true)]
    public class Plugin : IMvxPlugin
    {
        public void Load()
        {
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IExCrypto, ExCrypto>();
        }
    }
}