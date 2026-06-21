using FCGames.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Fiap.FCGames.Notifications.Worker.Consumers;

public class PagamentoProcessadoEventoConsumer : IConsumer<PagamentoProcessadoEvento>
{
    private readonly ILogger<PagamentoProcessadoEventoConsumer> _logger;

    public PagamentoProcessadoEventoConsumer(ILogger<PagamentoProcessadoEventoConsumer> logger)
        => _logger = logger;

    public Task Consume(ConsumeContext<PagamentoProcessadoEvento> context)
    {
        var evt = context.Message;

        if (evt.Status == "Aprovado")
        {
            _logger.LogInformation(
                "Notificacao: {Tipo} | jogo: {NomeJogo} | valor: {Valor} | usuarioId: {UsuarioId} | pedidoId: {PedidoId} | correlationId: {CorrelationId}",
                "email-confirmacao-compra",
                evt.NomeJogo,
                evt.Preco,
                evt.UsuarioId,
                evt.PedidoId,
                evt.CorrelationId);
        }
        else
        {
            _logger.LogInformation(
                "Notificacao: {Tipo} | jogo: {NomeJogo} | motivo: {Motivo} | usuarioId: {UsuarioId} | pedidoId: {PedidoId} | correlationId: {CorrelationId}",
                "email-pagamento-rejeitado",
                evt.NomeJogo,
                evt.Motivo,
                evt.UsuarioId,
                evt.PedidoId,
                evt.CorrelationId);
        }

        return Task.CompletedTask;
    }
}
