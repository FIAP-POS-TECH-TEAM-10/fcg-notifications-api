using MediatR;

namespace Fiap.FCGames.Notifications.Application.Queries.Usuario.BuscarUsuarioPorId;

public record BuscarUsuarioPorIdQuery(Guid Id) : IRequest<DetalhesUsuarioDto>;
