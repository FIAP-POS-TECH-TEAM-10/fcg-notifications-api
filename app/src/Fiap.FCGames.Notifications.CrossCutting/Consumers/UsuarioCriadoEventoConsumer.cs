using FCGames.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fiap.FCGames.Notifications.CrossCutting.Consumers;

public class UsuarioCriadoEventoConsumer : IConsumer<UsuarioCriadoEvento>
{
    private readonly ILogger<UsuarioCriadoEventoConsumer> _logger;

    public UsuarioCriadoEventoConsumer(ILogger<UsuarioCriadoEventoConsumer> logger)
        => _logger = logger;

    public Task Consume(ConsumeContext<UsuarioCriadoEvento> context)
    {
        var evt = context.Message;

        _logger.LogInformation(
            "Notificacao: {Tipo} | destinatario: {Email} | usuarioId: {UsuarioId} | correlationId: {CorrelationId}",
            "email-boas-vindas",
            evt.Email,
            evt.UsuarioId,
            evt.CorrelationId);

        return Task.CompletedTask;
    }
}
