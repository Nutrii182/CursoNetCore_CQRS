using Dominio;

namespace Aplicacion.Contratos
{
    public interface IJwtGenerator
    {
         string CrearToken(Usuario usuario);
    }
}