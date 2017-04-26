using AutoMapper;

namespace Excalibur.Shared.ObjectConverter
{
    public class BaseObjectMapper<TSource, TDestination> : IObjectMapper<TSource, TDestination>
        where TDestination : new()
    {
        public virtual TDestination Map(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public virtual void UpdateDestination(TSource source, TDestination destination)
        {
            Mapper.Map(source, destination);
        }

        public virtual void UpdateSource(TDestination destination, TSource source)
        {
            Mapper.Map(destination, source);
        }
    }
}
