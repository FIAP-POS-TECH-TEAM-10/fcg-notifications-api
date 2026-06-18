using Fiap.FCGames.Notifications.Application;
using Fiap.FCGames.Notifications.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.FCGames.Notifications.CrossCutting.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
    {
        var appAssembly = typeof(IAssemblyMarker).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(appAssembly));

        // Registra todos os validators do assembly
        services.AddValidatorsFromAssembly(appAssembly);

        // Adiciona o pipeline de validação
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehaviors<,>));

        return services;
    }
}
