using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<Curso>{
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<CursoUnico, Curso>
        {
            private readonly CursosOnlineContext _context;

            public Handler(CursosOnlineContext context){
                _context = context;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null) {
                    //throw new Exception("No se encontro el curso");
                    throw new HandlerException(HttpStatusCode.NotFound, new {message = "No se encontro el curso"});
                }
                return curso;
            }
        }
    }
}