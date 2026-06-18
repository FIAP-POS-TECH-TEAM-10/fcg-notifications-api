using MediatR;

namespace Fiap.FCGames.Notifications.Application.Queries.Usuario.ListarUsuarios;

public record ListarUsuariosQuery : IRequest<IEnumerable<ListaUsuariosDto>>;
