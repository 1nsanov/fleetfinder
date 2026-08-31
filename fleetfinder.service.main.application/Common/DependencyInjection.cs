using System.Net.Http.Headers;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using fleetfinder.service.main.application.Common.Seed;
using fleetfinder.service.main.application.Common.Validation;
using fleetfinder.service.main.application.Services;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Common;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        #region Mapper

        services.AddScoped<IMapper, MapperService>();

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMapCodeGen<,>))
            .AddClasses(
                classes => classes.AssignableTo(typeof(IMapCodeGen<,>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMapToExistCodeGen<,>))
            .AddClasses(
                classes => classes.AssignableTo(typeof(IMapCodeGen<,>)),
                publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        #endregion

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection), includeInternalTypes: true);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.Configure<PasswordOptions>(configuration.GetSection(PasswordOptions.SectionName));
        services.AddSingleton<IPasswordService, PasswordService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentifyService, IdentifyService>();

        services.Configure<SeedOptions>(configuration.GetSection(SeedOptions.SectionName));
        services.AddHttpClient(DemoDataSeeder.HttpClientName, client =>
        {
            client.Timeout = TimeSpan.FromSeconds(60);
            client.DefaultRequestHeaders.UserAgent.ParseAdd("FleetFinderDemoSeeder/1.0");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/*"));
        });
        services.AddScoped<IDemoDataSeeder, DemoDataSeeder>();

        return services;
    }
}
