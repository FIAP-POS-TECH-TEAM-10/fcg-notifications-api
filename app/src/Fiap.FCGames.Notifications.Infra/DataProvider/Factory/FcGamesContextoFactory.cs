using Fiap.FCGames.Notifications.Infra.DataProvider.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fiap.FCGames.Notifications.Infra.DataProvider.Factory
{
    public class FcGamesContextoFactory : IDesignTimeDbContextFactory<FcGamesContexto>
    {
        public FcGamesContexto CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FcGamesContexto>();
            optionsBuilder.UseSqlite("Data Source=fcgames.db");

            return new FcGamesContexto(optionsBuilder.Options);
        }
    }
}
