using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Elimina : IRequest<Unit>{
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Elimina, Unit>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.Id);

                if(curso == null) throw new Exception("No se encontro el curso");

                _context.Remove(curso);
                var result = await _context.SaveChangesAsync();

                if(result > 0) return Unit.Value;

                throw new Exception("No se pudo eliminar");
            }
        }
    }
}