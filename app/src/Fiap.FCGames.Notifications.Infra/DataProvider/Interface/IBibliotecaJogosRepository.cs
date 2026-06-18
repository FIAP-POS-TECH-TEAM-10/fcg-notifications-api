using Fiap.FCGames.Notifications.Domain.Aggregates;

namespace Fiap.FCGames.Notifications.Infra.DataProvider.Interface;

public interface IBibliotecaJogosRepository
{
    void Adicionar(BibliotecaJogos biblioteca);
    Task<BibliotecaJogos?> ObterPorUsuarioIdAsync(UsuarioId usuarioId);
}
