using Fiap.FCGames.Notifications.Infra.DataProvider.Contexto;
using Fiap.FCGames.Notifications.Infra.DataProvider.Interface;

namespace Fiap.FCGames.Notifications.Infra.DataProvider.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly FcGamesContexto _context;
    public IUsuarioRepository UsuarioRepository { get; }
    public IBibliotecaJogosRepository BibliotecaJogosRepository { get; }

    public UnitOfWork(
        FcGamesContexto context,
        IUsuarioRepository usuarioRepository,
        IBibliotecaJogosRepository bibliotecaJogosRepository)
    {
        _context = context;
        UsuarioRepository = usuarioRepository;
        BibliotecaJogosRepository = bibliotecaJogosRepository;
    }

    public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => _context.SaveChangesAsync(cancellationToken);
}
