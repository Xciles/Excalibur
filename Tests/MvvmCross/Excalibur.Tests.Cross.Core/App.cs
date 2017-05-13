using System.Reflection;
using Excalibur.Shared.Business;
using Excalibur.Shared.ObjectConverter;
using Excalibur.Shared.Presentation;
using Excalibur.Shared.Storage.Providers;
using Excalibur.Tests.Cross.Core.Domain;
using Excalibur.Tests.Cross.Core.Observable;
using MvvmCross.Platform.IoC;

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

            RegisterAppStart<ViewModels.Core.FirstViewModel>();
        }

        public override void RegisterDependencies()
        {
            Container.Register<IObjectStorageProvider<int, MyTestDomain>, ObjectAsFileStorageProvider<int, MyTestDomain>>();

            Container.Register<IObjectMapper<MyTestDomain, MyTestObservable>, BaseObjectMapper<MyTestDomain, MyTestObservable>>();

            Container.Register<IListBusiness<int, MyTestDomain>, BaseListBusiness<int, MyTestDomain>>();

            Container.RegisterSingle<IPresentation<int, MyTestObservable, MyTestObservable>, BasePresentation<int, MyTestDomain, MyTestObservable, MyTestObservable>>();
        }
    }
}
