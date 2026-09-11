using FleetFinder.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();
await app.ConfigurePipelineAsync();

app.Run();
