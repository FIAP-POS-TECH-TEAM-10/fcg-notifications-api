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

// MassTransit roda como hosted service (background) — consome os eventos do RabbitMQ.
builder.Services.AddMassTransitRabbitMq(builder.Configuration);

// Web host mínimo: existe apenas para expor /health ao liveness/readiness probe do k8s.
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseCorrelationId();

app.MapHealthChecks("/health");

await app.RunAsync();
