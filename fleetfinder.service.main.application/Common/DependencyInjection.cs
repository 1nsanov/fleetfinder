using fleetfinder.service.main.application.Services;
using fleetfinder.service.main.application.Services.UserService;

namespace fleetfinder.service.main.application.Common;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        services.AddAutoMapper(typeof(DependencyInjection));

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection), includeInternalTypes: true);
        services.AddFluentValidationAutoValidation();

        #region Services

        services.AddScoped<IUserService, UserService>();

        #endregion
        
        return services;
    }
}