using System.Reflection;
using System.Text.Json.Serialization;
using fleetfinder.service.main.application.Common;
using fleetfinder.service.main.application.Common.Middlewares;
using fleetfinder.service.main.application.Services;
using fleetfinder.service.main.infrastructure.Common;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace fleetfinder.service.main;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition =
                JsonIgnoreCondition.WhenWritingNull;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            var enumConverter = new JsonStringEnumConverter(allowIntegerValues: false);
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });

        builder.Services.AddDateOnlyTimeOnlyStringConverters();
        builder.Services.AddEndpointsApiExplorer();

        #region Swagger

        builder.Services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.CustomSchemaIds(type => type.FullName?.Replace("+", "_"));
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "fleetfinder.service.main", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        #endregion
        
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.RegisterInfrastructureLayer(builder.Configuration, builder.Environment);
        builder.Services.RegisterApplicationLayer(builder.Configuration);

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();

        app.UseCors(opt =>
        {
            opt.AllowAnyHeader();
            opt.AllowAnyOrigin();
            opt.AllowAnyMethod();
        });

        app.UseAuthentication();

        app.UseMiddleware<TokenServiceMiddleware>();
        
        app.UseAuthorization();

        app.UseEndpoints(endp =>
        {
            endp.MapControllers();
        });
        
        return app;
    }
}