using System.Collections.Generic;
using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerator
    {
         string CrearToken(Usuario usuario, List<string> roles);
    }
}