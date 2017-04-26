namespace Excalibur.Shared.ObjectConverter
{
    public interface IObjectMapper<TSource, TDestination>
    {
        TDestination Map(TSource source);
        void UpdateDestination(TSource source, TDestination destination);
        void UpdateSource(TDestination destination, TSource source);
    }
}
