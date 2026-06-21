using Fiap.FCGames.Notifications.CrossCutting.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCGames.Notifications.CrossCutting.Extensions;

public static class MassTransitExtensions
{
    public static IServiceCollection AddMassTransitRabbitMq(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<UsuarioCriadoEventoConsumer>();
            x.AddConsumer<PagamentoProcessadoEventoConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                var host = configuration["RabbitMQ:Host"] ?? "localhost";
                var username = configuration["RabbitMQ:Username"] ?? "guest";
                var password = configuration["RabbitMQ:Password"] ?? "guest";

                cfg.Host(host, "/", h =>
                {
                    h.Username(username);
                    h.Password(password);
                });

                cfg.ReceiveEndpoint("notifications-usuario-criado", e =>
                {
                    e.ConfigureConsumer<UsuarioCriadoEventoConsumer>(ctx);
                });

                cfg.ReceiveEndpoint("notifications-pagamento-processado", e =>
                {
                    e.ConfigureConsumer<PagamentoProcessadoEventoConsumer>(ctx);
                });
            });
        });

        return services;
    }
}
