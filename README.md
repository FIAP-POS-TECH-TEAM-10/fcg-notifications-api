# fcg-notifications-api

Microsserviço de notificações do **FCGames** (Tech Challenge FIAP — Fase 2).

Worker **puro e stateless**: não possui banco de dados nem CRUD HTTP. Apenas
consome eventos do RabbitMQ (via MassTransit) e produz **logs JSON estruturados**
(Serilog). A prova de funcionamento na demo é via `docker logs` / `kubectl logs`.

## Eventos consumidos

| Evento | Origem | Fila RabbitMQ | Log gerado |
|--------|--------|---------------|------------|
| `UsuarioCriadoEvento` | UsersAPI | `notifications-usuario-criado` | `tipo=email-boas-vindas`, destinatário, usuarioId, correlationId |
| `PagamentoProcessadoEvento` | PaymentsAPI | `notifications-pagamento-processado` | Aprovado: `tipo=email-confirmacao-compra` (jogo, valor); Rejeitado: `tipo=email-pagamento-rejeitado` (jogo, motivo) |

Os contratos vêm do pacote NuGet [`FCGames.IntegrationEvents`](https://github.com/FIAP-POS-TECH-TEAM-10/fcg-integration-events) — nunca duplicados localmente.

Exemplo de log (boas-vindas):

```
Notificacao: email-boas-vindas | destinatario: maria@exemplo.com | usuarioId: 3f2a... | correlationId: 9b1c...
```

## Endpoints

| Método | Rota | Auth | Descrição |
|--------|------|------|-----------|
| GET | `/health` | — | Liveness/readiness probe (k8s) |

Não há outros endpoints HTTP — o serviço é orientado a eventos.

## Variáveis de ambiente

| Variável | Default (dev) | Descrição |
|----------|---------------|-----------|
| `RabbitMQ__Host` | `localhost` | Host do RabbitMQ |
| `RabbitMQ__Username` | `guest` | Usuário RabbitMQ |
| `RabbitMQ__Password` | `guest` | Senha RabbitMQ |

> Em produção, RabbitMQ é exposto via env vars (nunca hardcoded no appsettings).
> O `nuget.config` usa `%NUGET_AUTH_TOKEN%` (PAT com scope `read:packages`) para
> restaurar o pacote `FCGames.IntegrationEvents` do feed do GitHub Packages.

## Rodando localmente

Pré-requisitos: .NET 10 SDK e um RabbitMQ acessível (ex.: `docker run -d -p 5672:5672 -p 15672:15672 rabbitmq:3-management`).

```bash
cd app/src
dotnet restore Fiap.FCGames.Notifications.slnx   # requer NUGET_AUTH_TOKEN se o pacote não estiver em cache
dotnet build   Fiap.FCGames.Notifications.slnx
dotnet run --project Fiap.FCGames.Notifications.Worker   # escuta em http://localhost:5004
```

## Estrutura

```
app/src/
  Fiap.FCGames.Notifications.Worker/       entry point, consumers e /health
    Consumers/                             UsuarioCriado + PagamentoProcessado (apenas logs)
  Fiap.FCGames.Notifications.CrossCutting/
    Middleware/                            propagação do x-correlation-ID nos logs
```

> O host (`.Worker`) usa o SDK web (`Microsoft.NET.Sdk.Web`) **apenas** para expor o
> endpoint `/health` exigido pelos probes do k8s; o MassTransit roda como hosted
> service em background. Não há controllers nem pipeline MVC.

> Serviço **stateless** e orientado a eventos — sem `Infra`/EF Core, e sem as camadas
> `Application`/`Domain` (não há CQRS, validação HTTP nem regras de domínio aqui).
