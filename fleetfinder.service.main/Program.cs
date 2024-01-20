using fleetfinder.service.main;

var builder = WebApplication.CreateBuilder(args);

// Test pullrequest
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();