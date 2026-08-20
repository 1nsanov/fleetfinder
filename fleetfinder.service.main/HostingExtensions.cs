using System.Reflection;
using System.Text.Json.Serialization;
using fleetfinder.service.main.application.Common;
using fleetfinder.service.main.application.Common.Middlewares;
using fleetfinder.service.main.application.Common.Seed;
using fleetfinder.service.main.infrastructure.Common;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi;

namespace fleetfinder.service.main;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            var enumConverter = new JsonStringEnumConverter(allowIntegerValues: false);
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });

        builder.Services.AddEndpointsApiExplorer();

        #region Swagger

        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);
            options.SupportNonNullableReferenceTypes();
            options.CustomSchemaIds(GetSchemaId);
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });
            options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
            {
                [new OpenApiSecuritySchemeReference("Bearer", document)] = []
            });
        });

        #endregion
        
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.RegisterInfrastructureLayer(builder.Configuration, builder.Environment);
        builder.Services.RegisterApplicationLayer(builder.Configuration);
        
        builder.Services.Configure<FormOptions>(o =>
        {
            o.ValueCountLimit = 10;
            o.ValueLengthLimit = int.MaxValue;  
            o.MultipartBodyLengthLimit = long.MaxValue;
        });  
        
        return builder.Build();
    }

    public static async Task<WebApplication> ConfigurePipelineAsync(this WebApplication app)
    {
        app.Services.ApplyMigrations();
        await app.Services.SeedDemoDataAsync();

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

        app.MapControllers();

        app.MapGet("api/ping", () => "pong");
        
        return app;
    }

    private static string GetSchemaId(Type type)
    {
        if (type.IsGenericParameter)
            return type.Name;

        var names = new Stack<string>();
        for (var current = type; current != null; current = current.DeclaringType)
        {
            var name = current.Name;
            var tick = name.IndexOf('`');
            if (tick >= 0)
                name = name[..tick];
            names.Push(name);
        }

        var id = string.Join("_", names);
        if (!type.IsGenericType)
            return id;

        var args = string.Join("_", type.GetGenericArguments().Select(GetSchemaId));
        return $"{id}_{args}";
    }
}
