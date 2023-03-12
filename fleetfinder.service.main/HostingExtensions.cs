using System.Reflection;
using System.Text.Json.Serialization;
using fleetfinder.service.main.application.Common;
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
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });
        
        
        builder.Services.AddDateOnlyTimeOnlyStringConverters();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();
            options.CustomSchemaIds(type => type.FullName?.Replace("+", "_"));
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "fleetfinder.service.main", Version = "v1" });
        });
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.RegisterInfrastructureLayer(builder.Configuration, builder.Environment);
        builder.Services.RegisterApplicationLayer();

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

        app.UseAuthorization();

        app.UseEndpoints(endp =>
        {
            endp.MapControllers();
        });
        
        return app;
    }
}