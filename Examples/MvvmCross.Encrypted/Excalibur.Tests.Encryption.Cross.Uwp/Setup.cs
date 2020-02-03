using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Excalibur.MvvmCross.Plugin.ProtectedStore;
using Excalibur.MvvmCross.Plugin.ProtectedStore.Platforms.Uap;
using Excalibur.MvvmCross.Plugin.RootChecker;
using Excalibur.MvvmCross.Plugin.RootChecker.Platforms.Uap;
using Excalibur.Providers.FileStorage;
using Excalibur.Tests.Encrypted.Cross.Uwp.Controls;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Localization;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Plugin;
using MvvmCross.Plugin.Json;
using MvvmCross.Plugin.ResourceLoader.Platforms.Uap;
using MvvmCross.ViewModels;
namespace Excalibur.Tests.Encrypted.Cross.Uwp
{
    public class ExRootChecker : RootChecker
    {
        public override bool IsRooted()
        {
#if __SIM__
			return false;
#endif
#if !__SIM__
            return false;
#endif
        }
    }

    public class Setup : MvxWindowsSetup<Encrypted.Cross.Core.App>
    {
        public Setup() : base()
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Encrypted.Cross.Core.App();
        }

        // Workaround for https://github.com/MvvmCross/MvvmCross/issues/2678
        protected override IEnumerable<System.Reflection.Assembly> ValueConverterAssemblies
        {
            get
            {
                var toReturn = base.ValueConverterAssemblies.ToList();
                toReturn.Add(typeof(MvxLanguageConverter).Assembly);
                return toReturn;
            }
        }
        public override IEnumerable<Assembly> GetPluginAssemblies()
        {
            var assemblies = base.GetPluginAssemblies().ToList();
            assemblies.Add(typeof(global::MvvmCross.Plugin.File.Platforms.Uap.Plugin).Assembly);
            assemblies.Add(typeof(global::MvvmCross.Plugin.Json.Plugin).Assembly);
            assemblies.Add(typeof(global::MvvmCross.Plugin.ResourceLoader.Platforms.Uap.Plugin).Assembly);

            assemblies.Add(typeof(MvvmCross.Plugin.RootChecker.Platforms.Uap.Plugin).Assembly);
            assemblies.Add(typeof(MvvmCross.Plugin.ProtectedStore.Platforms.Uap.Plugin).Assembly);
            assemblies.Add(typeof(Providers.FileStorage.Plugin).Assembly);
            assemblies.Add(typeof(Providers.Encryption.Plugin).Assembly);
            assemblies.Add(typeof(Providers.EncryptedFileStorage.Plugin).Assembly);
            return assemblies;
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<MvxPresentationHint, MvxPanelPopToRootPresentationHint>();

        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<IRootChecker, ExRootChecker>();
        }
    }
}
