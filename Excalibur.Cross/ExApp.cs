using Excalibur.Cross.Business;
using Excalibur.Cross.Configuration;
using Excalibur.Cross.Storage;
using MvvmCross;
using MvvmCross.ViewModels;

namespace Excalibur.Cross
{
    /// <summary>
    /// Excalibur based <see cref="MvxApplication"/>. 
    /// This will initialize internal container, register dependencies and provide some useful methods.
    /// 
    /// The App of an application should implement this class.
    /// </summary>
    public abstract class ExApp : MvxApplication
    {
        /// <summary>
        /// The MvxApplication Initialize. 
        /// This method is used to register default dependencies and register the internal container.
        /// 
        /// Please use <see cref="RegisterDependencies"/> to register dependencies that need to be registered for Excalibur
        /// </summary>
        public override void Initialize()
        {
            // Register services
            RegisterExcaliburInternal();

            RegisterDependencies();

            base.Initialize();
        }

        /// <summary>
        /// Method that should be used to register dependencies that are required for Excalibur to work. 
        /// </summary>
        public abstract void RegisterDependencies();

        /// <summary>
        /// Not sure yet...
        /// </summary>
        /// <typeparam name="TId"></typeparam>
        /// <typeparam name="TDomain"></typeparam>
        /// <param name="type"></param>
        public void UseObjectProvider<TId, TDomain>(IObjectStorageProvider<TId, TDomain> type)
            where TDomain : StorageDomain<TId>
        {
            Mvx.RegisterType<IObjectStorageProvider<TId, TDomain>>(() => type);
        }

        /// <summary>
        /// Used for registering internal Excalibur dependencies
        /// </summary>
        private void RegisterExcaliburInternal()
        {
            Mvx.RegisterType<IStorageService, ExStorageService>();
            Mvx.RegisterType<IExMainThreadDispatcher, ExMainThreadDispatcher>();
            Mvx.RegisterType<IConfigurationManager, ConfigurationManager>();

            // Register business based on domain entities
            // Register Services based on domain entities
            // Register presentation based on Domain / Observable AS singleton
            // Register Iobjectmappers based on domain / Observables

        }
    }
}
