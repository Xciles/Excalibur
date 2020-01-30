using System;
using Excalibur.Base.Observable;
using Excalibur.Base.Providers;
using Excalibur.Cross.Extensions;
using Excalibur.Cross.ObjectConverter;
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
                .WithDefaultMappers();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper(options =>
                {
                    options.DefaultDomainMapper();
                    options.DefaultObservableMapper();
                });

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

    public class Class1
    {
        public void TryOut()
        {
            IMvxIoCProvider ioc = new MvxIoCContainer(new MvxIoCContainer(new MvxIocOptions()));

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithDefaultMappers();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper()
                    .DefaultDomainMapper()
                    .DefaultObservableMapper()
                    .MapperCompleteAsSingle();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper()
                    .DomainMapper<CustomMapper>()
                    .DefaultObservableMapper()
                    .MapperCompleteAsSingle();

            ioc
                .RegisterExcaliburSingleEntity<int, DEntity, OEntity>()
                .WithMapper2(options =>
                {
                    options.Test();
                    options.Bla();
                });



        }

    }
}
