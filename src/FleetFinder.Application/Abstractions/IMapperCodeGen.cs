namespace FleetFinder.Application.Abstractions
{
    internal interface IMapCodeGen<TSource, TDestination>
    {
        TDestination Map(TSource source);
    }

    internal interface IMapToExistCodeGen<TSource, TDestination>
    {
        TDestination Map(TSource source, TDestination destination);
    }
}
