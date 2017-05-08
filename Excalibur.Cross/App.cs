using System.Reflection;
using Excalibur.Cross.Storage;
using Excalibur.Shared.Storage;
using MvvmCross.Core.ViewModels;
using XLabs.Ioc;

namespace Excalibur.Cross
{
    public abstract class ExApp : MvxApplication
    {
        public override void Initialize()
        {
            // Register services


            base.Initialize();
        }

        public void RegisterExcalibur(Assembly current)
        {
            var container = new SimpleContainer();
            container.Register<IStorageService, ExStorageService>();

            // Register business based on domain entities
            // Register Services based on domain entities
            // Register presentation based on Domain / Observable AS singleton
            // Register Iobjectmappers based on domain / Observables


            Resolver.SetResolver(container.GetResolver());
        }
    }
}
