using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCGames.Notifications.CrossCutting.Extensions;

public static class RegisterDependencyInjectionExtensions
{
    public static void RegisterDI(this IServiceCollection services)
    {
        // NotificationsAPI é stateless — sem banco de dados, sem repositories
        // TODO: registrar consumers MassTransit quando implementados
    }
}
