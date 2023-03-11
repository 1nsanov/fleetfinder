using System.Text.Json.Serialization;
using fleetfinder.service.main.infrastructure.Common;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

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
            var enumConverter = new JsonStringEnumConverter();
            options.JsonSerializerOptions.Converters.Add(enumConverter);
        });
        
        
        builder.Services.AddDateOnlyTimeOnlyStringConverters();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddFluentValidationRulesToSwagger();

        builder.Services.RegisterInfrastructureLayer(builder.Configuration, builder.Environment);

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