namespace Fiap.FCGames.Notifications.Application.Commands.Usuario.AtualizarUsuario;

public record AtualizarUsuarioResponse
{
    public Guid Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
}
