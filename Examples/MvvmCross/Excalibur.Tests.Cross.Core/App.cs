using Excalibur.Shared.Business;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage.Providers;
using Excalibur.Tests.Cross.Core.Services;
using MvvmCross.Platform.IoC;
using System.Collections.Generic;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Excalibur.Tests.Cross.Core
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

            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            var appStart = Mvx.Resolve<IMvxAppStart>();

            RegisterAppStart(appStart);
        }

        public override void RegisterDependencies()
        {
            // User
            Container.Register<IObjectStorageProvider<int, Domain.User>, ObjectAsFileStorageProvider<int, Domain.User>>();

            Container.Register<IObjectMapper<Domain.User, Observable.User>, BaseObjectMapper<Domain.User, Observable.User>>();
            Container.Register<IObjectMapper<Observable.User, Observable.User>, BaseObjectMapper<Observable.User, Observable.User>>();

            Container.Register<IListBusiness<int, Domain.User>, BaseListBusiness<int, Domain.User>>();

            Container.Register<IServiceBase<IList<Domain.User>>, UserService>();

            Container.RegisterSingle<IPresentation<int, Observable.User, Observable.User>, BasePresentation<int, Domain.User, Observable.User, Observable.User>>();

            // Todo
            Container.Register<IObjectStorageProvider<int, Domain.Todo>, ObjectAsFileStorageProvider<int, Domain.Todo>>();

            Container.Register<IObjectMapper<Domain.Todo, Observable.Todo>, BaseObjectMapper<Domain.Todo, Observable.Todo>>();
            Container.Register<IObjectMapper<Observable.Todo, Observable.Todo>, BaseObjectMapper<Observable.Todo, Observable.Todo>>();

            Container.Register<IListBusiness<int, Domain.Todo>, BaseListBusiness<int, Domain.Todo>>();

            Container.Register<IServiceBase<IList<Domain.Todo>>, TodoService>();

            Container.RegisterSingle<IPresentation<int, Observable.Todo, Observable.Todo>, BasePresentation<int, Domain.Todo, Observable.Todo, Observable.Todo>>();
        }
    }
}
