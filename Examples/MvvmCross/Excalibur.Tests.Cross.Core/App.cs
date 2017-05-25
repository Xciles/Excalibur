using System.Reflection;
using Excalibur.Shared.Business;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Services;
using Excalibur.Shared.Storage.Providers;
using Excalibur.Tests.Cross.Core.Domain;
using Excalibur.Tests.Cross.Core.Observable;
using Excalibur.Tests.Cross.Core.Services;
using MvvmCross.Platform.IoC;
using System.Collections.Generic;
using Excalibur.Tests.Cross.Core.ViewModels;
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
            // TestObservable
            Container.Register<IObjectStorageProvider<int, MyTestDomain>, ObjectAsFileStorageProvider<int, MyTestDomain>>();

            Container.Register<IObjectMapper<MyTestDomain, MyTestObservable>, BaseObjectMapper<MyTestDomain, MyTestObservable>>();
            Container.Register<IObjectMapper<MyTestObservable, MyTestObservable>, BaseObjectMapper<MyTestObservable, MyTestObservable>>();

            Container.Register<IListBusiness<int, MyTestDomain>, BaseListBusiness<int, MyTestDomain>>();

            Container.Register<IServiceBase<IList<MyTestDomain>>, MyTestService>();

            Container.RegisterSingle<IPresentation<int, MyTestObservable, MyTestObservable>, BasePresentation<int, MyTestDomain, MyTestObservable, MyTestObservable>>();

            // User
            Container.Register<IObjectStorageProvider<int, Domain.User>, ObjectAsFileStorageProvider<int, Domain.User>>();

            Container.Register<IObjectMapper<Domain.User, Observable.User>, BaseObjectMapper<Domain.User, Observable.User>>();
            Container.Register<IObjectMapper<Observable.User, Observable.User>, BaseObjectMapper<Observable.User, Observable.User>>();

            Container.Register<IListBusiness<int, Domain.User>, BaseListBusiness<int, Domain.User>>();

            Container.Register<IServiceBase<IList<Domain.User>>, UserService>();

            Container.RegisterSingle<IPresentation<int, Observable.User, Observable.User>, BasePresentation<int, Domain.User, Observable.User, Observable.User>>();
        }
    }
}
