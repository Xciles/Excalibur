using System.Collections.Generic;
using System.IO;
using System.Linq;
using Excalibur.Tests.Cross.Uwp.Controls;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Localization;
using MvvmCross.Platforms.Uap.Core;
using MvvmCross.Plugin.File;
using MvvmCross.Plugin.File.Platforms.Uap;
using MvvmCross.Plugin.Json;
using MvvmCross.Plugin.ResourceLoader.Platforms.Uap;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.Cross.Uwp
{

    public class Setup : MvxWindowsSetup<Core.App>
    {
        public Setup() : base()
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
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

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.ConstructAndRegisterSingleton<MvxPresentationHint, MvxPanelPopToRootPresentationHint>();

            Mvx.IoCProvider.RegisterSingleton<IMvxJsonConverter>(new MvxJsonConverter());
            Mvx.IoCProvider.RegisterType<IMvxFileStore, MvxWindowsFileStore>();
            Mvx.IoCProvider.RegisterType<IMvxFileStoreAsync, MvxWindowsFileStore>();
            Mvx.IoCProvider.RegisterType<IMvxResourceLoader, MvxStoreResourceLoader>();
        }
    }
}
