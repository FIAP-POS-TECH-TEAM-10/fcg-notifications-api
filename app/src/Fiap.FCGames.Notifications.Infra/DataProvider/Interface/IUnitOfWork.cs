namespace Fiap.FCGames.Notifications.Infra.DataProvider.Interface;

public interface IUnitOfWork
{
    IUsuarioRepository UsuarioRepository { get; }
    IBibliotecaJogosRepository BibliotecaJogosRepository { get; }
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
