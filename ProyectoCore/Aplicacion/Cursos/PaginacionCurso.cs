using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Paginacion;

namespace Aplicacion.Cursos
{
    public class PaginacionCurso
    {
        public class Ejecuta : IRequest<PaginacionModel>{
            public string Titulo {get;set;}
            public int NumeroPagina {get;set;}
            public int CantidadElementos {get;set;}
        }

        public class Manejador : IRequestHandler<Ejecuta, PaginacionModel>
        {
            private readonly IPaginacion _paginacion;
            public Manejador(IPaginacion paginacion){
                _paginacion = paginacion;
            }
            public async Task<PaginacionModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var storedProcedure = "usp_obtener_curso_paginacion";
                var ordenamiento = "Titulo";
                var parametros = new Dictionary<string,object>();
                parametros.Add("NombreCurso", request.Titulo);
                return await _paginacion.devolverPaginacion(storedProcedure, request.NumeroPagina, request.CantidadElementos,parametros,ordenamiento);

            }
        }
    }
}