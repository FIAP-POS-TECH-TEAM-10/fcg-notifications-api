namespace Fiap.FCGames.Notifications.Domain.Aggregates;

public record struct BibliotecaJogosId(Guid Value)
{
    public static BibliotecaJogosId New() => new(Guid.NewGuid());
    public override readonly string ToString() => Value.ToString();
}
