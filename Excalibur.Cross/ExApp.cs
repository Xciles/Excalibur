using System.Reflection;
using Excalibur.Cross.Storage;
using Excalibur.Shared.Storage;
using Excalibur.Shared.Storage.Providers;
using MvvmCross.Core.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Cross
{
    public abstract class ExApp : MvxApplication
    {
        protected SimpleContainer Container { get; set; }

        protected ExApp()
        {
            Container = new SimpleContainer();
        }

        public override void Initialize()
        {
            // Register services
            RegisterExcaliburInternal();
            RegisterDependencies();

            Resolver.SetResolver(Container.GetResolver());

            base.Initialize();
        }

        public abstract void RegisterDependencies();

        public void UseObjectProvider<TId, TDomain>(IObjectStorageProvider<TId, TDomain> type)
            where TDomain : StorageDomain<TId>
        {
            Container.Register<IObjectStorageProvider<TId, TDomain>>(type);
        }

        private void RegisterExcaliburInternal()
        {
            Container.Register<IStorageService, ExStorageService>();

            // Register business based on domain entities
            // Register Services based on domain entities
            // Register presentation based on Domain / Observable AS singleton
            // Register Iobjectmappers based on domain / Observables

        }
    }
}
