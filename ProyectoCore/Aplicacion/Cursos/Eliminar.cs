using System;
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
    public class Eliminar
    {
        public class Elimina : IRequest<Unit>{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Elimina, Unit>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.Id);

                foreach(var i in instructoresDB){
                    _context.CursoInstructor.Remove(i);
                }

                var comentariosDB = _context.Comentario.Where(x => x.CursoId == request.Id);
                foreach(var cmt in comentariosDB){
                    _context.Comentario.Remove(cmt);
                }

                var precioDb = _context.Precio.Where(x => x.CursoId == request.Id).FirstOrDefault();
                if(precioDb != null){
                    _context.Precio.Remove(precioDb);
                }

                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null) {
                    //throw new Exception("No se encontro el curso");
                    throw new HandlerException(HttpStatusCode.NotFound, new {message = "No se encontro el curso"});
                }
                _context.Remove(curso);
                var result = await _context.SaveChangesAsync();

                if(result > 0) return Unit.Value;

                throw new Exception("No se pudo eliminar");
            }
        }
    }
}