using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest{
            public int CursoId { get; set; }
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime? FechaPublicacion {get; set;}
            //public byte[] FotoPortada { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta>{
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context){
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);

                if(curso == null) throw new Exception("No se encontró el curso");

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var valor = await _context.SaveChangesAsync();

                if(valor > 0) return Unit.Value;

                throw new Exception("No se pudo actualizar");
            }
        }
    }
}