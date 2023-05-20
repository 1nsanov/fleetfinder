using System.Text;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Validation;
using fleetfinder.service.main.application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace fleetfinder.service.main.application.Common;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(conf => conf.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));

        #region Mapper
        
        services.AddScoped<IMapper, MapperService>();

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMapCodeGen<,>))
            .AddClasses(classes => classes.AssignableTo(typeof(IMapCodeGen<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(IMapToExistCodeGen<,>))
            .AddClasses(classes => classes.AssignableTo(typeof(IMapToExistCodeGen<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        
        #endregion

        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection), includeInternalTypes: true);
        services.AddFluentValidationAutoValidation();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        #region Services

        services.AddSingleton<TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentifyService, IdentifyService>();
        services.AddScoped<IImageService, ImageService>();
        
        #endregion

        #region JWT

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                        configuration["Jwt:Key"]))
                };
            });
        #endregion
        
        return services;
    }
}