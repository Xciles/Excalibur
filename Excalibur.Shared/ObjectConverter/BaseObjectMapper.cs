using AutoMapper;

namespace Excalibur.Shared.ObjectConverter
{
    public class BaseObjectMapper<TSource, TDestination> : IObjectMapper<TSource, TDestination>
        where TDestination : new()
    {
        private readonly MapperConfiguration _config;

        public BaseObjectMapper()
        {
            _config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TSource, TDestination>();
            });
        }

        public virtual TDestination Map(TSource source)
        {
            IMapper mapper = _config.CreateMapper();
            return mapper.Map<TSource, TDestination>(source);
        }

        public virtual void UpdateDestination(TSource source, TDestination destination)
        {
            IMapper mapper = _config.CreateMapper();
            mapper.Map(source, destination);
        }

        public virtual void UpdateSource(TDestination destination, TSource source)
        {
            IMapper mapper = _config.CreateMapper();
            mapper.Map(destination, source);
        }
    }
}
