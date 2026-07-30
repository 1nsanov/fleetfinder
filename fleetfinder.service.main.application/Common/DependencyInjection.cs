using System.Text;
using Amazon.S3;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using fleetfinder.service.main.application.Common.Options;
using fleetfinder.service.main.application.Common.Validation;
using fleetfinder.service.main.application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
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

        services.Configure<PasswordOptions>(configuration.GetSection(PasswordOptions.SectionName));
        services.AddSingleton<IPasswordService, PasswordService>();

        services.Configure<S3StorageOptions>(configuration.GetSection(S3StorageOptions.SectionName));
        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<S3StorageOptions>>().Value;
            var config = new AmazonS3Config
            {
                ServiceURL = options.ServiceUrl,
                ForcePathStyle = true,
                AuthenticationRegion = "us-east-1"
            };
            return new AmazonS3Client(options.AccessKey, options.SecretKey, config);
        });
        services.AddSingleton<IObjectStorageService, S3ObjectStorageService>();

        services.AddSingleton<TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IIdentifyService, IdentifyService>();
        
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