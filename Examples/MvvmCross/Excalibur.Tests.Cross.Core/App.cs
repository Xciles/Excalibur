using Excalibur.Shared.Business;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Services;
using Excalibur.Tests.Cross.Core.Services;
using System.Collections.Generic;
using Excalibur.Tests.Cross.Core.Services.Interfaces;
using Excalibur.Tests.Cross.Core.State;
using XLabs.Ioc;
using Excalibur.Shared.Storage;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using MvvmCross;

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

            var state = Resolver.Resolve<IApplicationState>();
            state.InitAndLoadAsync().GetAwaiter().GetResult();

            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();
            var appStart = Mvx.Resolve<IMvxAppStart>();

            RegisterAppStart(appStart);
        }

        public override void RegisterDependencies()
        {
            // Application Things
            Container.RegisterSingle<IApplicationState, ApplicationState>();
            Container.RegisterSingle<ISyncService, SyncService>();

            // User
            Container.Register<IObjectStorageProvider<int, Domain.LoggedInUser>, ObjectAsFileStorageProvider<int, Domain.LoggedInUser>>();

            Container.Register<IObjectMapper<Domain.LoggedInUser, Observable.LoggedInUser>, BaseObjectMapper<Domain.LoggedInUser, Observable.LoggedInUser>>();
            Container.Register<IObjectMapper<Observable.LoggedInUser, Observable.LoggedInUser>, BaseObjectMapper<Observable.LoggedInUser, Observable.LoggedInUser>>();

            Container.Register<Business.Interfaces.ILoggedInUser, Business.LoggedInUser>();

            Container.Register<IServiceBase<Domain.LoggedInUser>, LoggedInUserService>();

            Container.RegisterSingle<ISinglePresentation<int, Observable.LoggedInUser>, BaseSinglePresentation<int, Domain.LoggedInUser, Observable.LoggedInUser>>();

            // User
            Container.Register<IObjectStorageProvider<int, Domain.User>, ObjectAsFileStorageProvider<int, Domain.User>>();

            Container.Register<IObjectMapper<Domain.User, Observable.User>, BaseObjectMapper<Domain.User, Observable.User>>();
            Container.Register<IObjectMapper<Observable.User, Observable.User>, BaseObjectMapper<Observable.User, Observable.User>>();

            Container.Register<IListBusiness<int, Domain.User>, BaseListBusiness<int, Domain.User>>();

            Container.Register<IServiceBase<IList<Domain.User>>, UserService>();

            Container.RegisterSingle<IListPresentation<int, Observable.User, Observable.User>, BaseListPresentation<int, Domain.User, Observable.User, Observable.User>>();

            // Todo
            Container.Register<IObjectStorageProvider<int, Domain.Todo>, ObjectAsFileStorageProvider<int, Domain.Todo>>();

            Container.Register<IObjectMapper<Domain.Todo, Observable.Todo>, BaseObjectMapper<Domain.Todo, Observable.Todo>>();
            Container.Register<IObjectMapper<Observable.Todo, Observable.Todo>, BaseObjectMapper<Observable.Todo, Observable.Todo>>();

            Container.Register<IListBusiness<int, Domain.Todo>, BaseListBusiness<int, Domain.Todo>>();

            Container.Register<IServiceBase<IList<Domain.Todo>>, TodoService>();

            Container.RegisterSingle<Presentation.Interfaces.ITodo, Presentation.Todo>();
        }
    }
}
