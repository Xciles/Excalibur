using System;
using System.Threading.Tasks;
using Excalibur.Cross.Business;
using Excalibur.Cross.Extensions;
using Excalibur.Cross.ObjectConverter;
using Excalibur.Cross.Observable;
using Excalibur.Cross.Presentation;
using Excalibur.Cross.Providers;
using Excalibur.Cross.Services;
using MvvmCross.IoC;

namespace Excalibur.GeneralTests
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IMvxIoCProvider ioc = new MvxIoCContainer(new MvxIoCContainer(new MvxIocOptions()));

            //ioc
            //    .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
            //    .WithMapper()
            //        .DefaultDomainMapper()
            //        .DefaultObservableMapper()
            //        .MapperCompleteAsSingle();

            //ioc
            //    .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
            //    .WithMapper()
            //        .DomainMapper<CustomMapper>()
            //        .DefaultObservableMapper()
            //        .MapperCompleteAsSingle();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithDefault()
                .WithDefaultService<CustomService>();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithDefaultMappers()
                .WithBusiness<ISomeBusiness, SomeBusiness>()
                .WithPresentation<ISomePresentation, SomePresentation>()
                .WithService<ICustomService, CustomService>();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper(options =>
                {
                    options.DefaultDomainMapper();
                    options.DefaultObservableMapper();
                })
                .WithBusiness<ISomeBusiness, SomeBusiness>()
                .WithPresentation<ISomePresentation, SomePresentation>()
                .WithService<ICustomService, CustomService>();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper(options =>
                {
                    options.DomainMapper<CustomMapper>();
                    options.DefaultObservableMapper();
                });
        }
    }


    public class DEntity : ProviderDomain<int> { }
    public class OEntity : ObservableBase<int> { }
    public class CustomMapper : BaseObjectMapper<DEntity, OEntity> { }
    public interface ISomeBusiness : ISingleBusiness<DEntity> { }
    public class SomeBusiness : BaseSingleBusiness<int, DEntity>, ISomeBusiness {
        public SomeBusiness(IServiceBase<DEntity> service, IDatabaseProvider<int, DEntity> storageProvider) : base(service, storageProvider)
        {
        }
    }
    public interface ISomePresentation : ISinglePresentation<int, OEntity> { }
    public class SomePresentation : BaseSinglePresentation<int, DEntity, OEntity>, ISomePresentation {
        public SomePresentation(IObjectMapper<DEntity, OEntity> domainSelectedMapper) : base(domainSelectedMapper)
        {
        }
    }
    public interface ICustomService : IServiceBase<DEntity> { }
    public class CustomService : ICustomService
    {
        public Task<DEntity> SyncData() => throw new NotImplementedException();
    }

    //public class Class1
    //{
    //    public void TryOut()
    //    {
    //        IMvxIoCProvider ioc = new MvxIoCContainer(new MvxIoCContainer(new MvxIocOptions()));

    //        ioc
    //            .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
    //            .WithDefaultMappers();

    //        ioc
    //            .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
    //            .WithMapper()
    //                .DefaultDomainMapper()
    //                .DefaultObservableMapper()
    //                .MapperCompleteAsSingle();

    //        ioc
    //            .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
    //            .WithMapper()
    //                .DomainMapper<CustomMapper>()
    //                .DefaultObservableMapper()
    //                .MapperCompleteAsSingle();

    //        ioc
    //            .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
    //            .WithMapper2(options =>
    //            {
    //                options.Test();
    //                options.Bla();
    //            });



    //    }

    //}
}
