using Fiap.FCGames.Notifications.Domain.Interface.Service;
using Fiap.FCGames.Notifications.Infra.DataProvider.Interface;
using Fiap.FCGames.Notifications.Infra.DataProvider.Repositories;
using Fiap.FCGames.Notifications.Infra.DataProvider.UnitOfWork;
using Fiap.FCGames.Notifications.Infra.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCGames.Notifications.CrossCutting.Extensions;

public static class RegisterDependencyInjectionExtensions
{
    public static void RegisterDI(this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IBibliotecaJogosRepository, BibliotecaJogosRepository>();

        // Unit of Work
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
    }
}
