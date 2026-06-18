using MediatR;

namespace Fiap.FCGames.Notifications.Application.Commands.Login;

public record LoginCommand(string Usuario, string Senha) : IRequest<UsuarioLogadoDto>;
