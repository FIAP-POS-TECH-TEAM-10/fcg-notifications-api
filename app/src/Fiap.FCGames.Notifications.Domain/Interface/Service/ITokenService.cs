using System;
using System.Collections.Generic;
using System.Text;

namespace Fiap.FCGames.Notifications.Domain.Interface.Service
{
    public interface ITokenService
    {
        string GerarToken(string usuario, string role, DateTime loginExpiracao);
        bool ValidarToken(string token);
    }
}
