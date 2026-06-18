using Fiap.FCGames.Notifications.Domain.Aggregates;
using Fiap.FCGames.Notifications.Infra.DataProvider.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Fiap.FCGames.Notifications.Infra.DataProvider.Contexto
{
    public class FcGamesContexto : DbContext
    {
        public FcGamesContexto(DbContextOptions<FcGamesContexto> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<BibliotecaJogos> BibliotecaJogos { get; set; }

        public DbSet<TipoAcessoUsuario> TipoAcessos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TipoAcessoUsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new BibliotecaJogosConfiguration());
        }
    }
}
