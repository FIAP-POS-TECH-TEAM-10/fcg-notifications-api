using Fiap.FCGames.Notifications.CrossCutting.Extensions;
using Fiap.FCGames.Notifications.CrossCutting.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.Configure<Microsoft.AspNetCore.Mvc.ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.AddHealthChecks();

builder.Services.AddMassTransitRabbitMq(builder.Configuration);

var app = builder.Build();

app.UseCorrelationId();
app.UseErrorHandlingMiddleware();

app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();
