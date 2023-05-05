namespace fleetfinder.service.main.application.Services
{
    internal sealed class MapperService : IMapper
    {
        private readonly IServiceProvider _serviceProvider;

        public MapperService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        TDestination IMapper.Map<TSource, TDestination>(TSource source)
        {
            return GetService<IMapCodeGen<TSource, TDestination>>().Map(source);
        }

        TDestination IMapper.Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return GetService<IMapToExistCodeGen<TSource, TDestination>>().Map(source, destination);
        }

        private TService GetService<TService>()
            where TService : notnull
        {
            return _serviceProvider.GetService<TService>()
                ?? throw new InvalidOperationException(
                    $"Mapper from type {typeof(TService).GenericTypeArguments[0]} to {typeof(TService).GenericTypeArguments[1]} is not registred.");
        }
    }
}
