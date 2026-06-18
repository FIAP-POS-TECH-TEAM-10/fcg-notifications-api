using MediatR;

namespace Fiap.FCGames.Notifications.Application.Queries.BibliotecaJogos.BuscarBibliotecaJogos;

public record BuscarBibliotecaJogosQuery(Guid UsuarioId) : IRequest<BuscarBibliotecaJogosResponse?>;
