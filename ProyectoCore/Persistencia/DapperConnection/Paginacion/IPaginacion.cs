using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistencia.DapperConnection.Paginacion
{
    public interface IPaginacion
    {
         Task<PaginacionModel> devolverPaginacion(
            string storeProcedure, 
            int numeroPagina, 
            int cantidadElementos, 
            IDictionary<string,object> parametrosFitro,
            string ordenamientoColumna
            );
    }
}