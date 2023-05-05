namespace fleetfinder.service.main.application.Common.Interfaces
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
