using System.Collections.Generic;
using System.Reflection;
using Excalibur.Base.Storage;
using Excalibur.Cross.Language;
using Excalibur.Providers.FileStorage;
using Excalibur.Tests.FormsCross.Core.State;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;

namespace Excalibur.Tests.FormsCross.Core
{
    public class App : Excalibur.Cross.ExApp
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            base.Initialize();

            var appStart = Mvx.IoCProvider.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();

            RegisterTextResources();
            RegisterAppStart(appStart);
        }

        public override void RegisterDependencies()
        {
            // Application Things
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IApplicationState, ApplicationState>();

            var config = new FileStorageConfiguration(new FileStorageConfig());
            config.Configure();
        }

        private void RegisterTextResources()
        {
            var assemblies = new List<Assembly>()
            {
                GetType().GetTypeInfo().Assembly,
            };
            ExTextProvider.InitializeAndCreateBuilder(assemblies, "AppResources/Text", "Excalibur.Tests", "Shared");
        }
    }
}
