using System.Net.Http.Headers;
using FleetFinder.Application.Abstractions.Identity;
using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Application.Common.Options;
using FleetFinder.Application.Common.Seed;
using FleetFinder.Application.Common.Validation;
using FleetFinder.Application.Services;
using Microsoft.Extensions.Configuration;

namespace FleetFinder.Application.Common;

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
        services.AddScoped<IIdentityService, IdentityService>();

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
