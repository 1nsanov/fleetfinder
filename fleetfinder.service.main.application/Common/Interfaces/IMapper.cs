namespace fleetfinder.service.main.application.Common.Interfaces
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource source)
            where TSource : notnull
            where TDestination : notnull;

        TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            where TSource : notnull
            where TDestination : notnull;
    }
}
