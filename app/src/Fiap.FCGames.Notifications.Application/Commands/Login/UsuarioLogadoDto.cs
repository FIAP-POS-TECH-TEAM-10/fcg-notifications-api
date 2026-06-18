namespace Fiap.FCGames.Notifications.Application.Commands.Login;

public class UsuarioLogadoDto
{
    public required string Usuario { get; set; }
    public required string Token { get; set; }
    public DateTime LoginExpiracao { get; set; }
}
