using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest{
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime FechaPublicacion {get; set;}
            public byte[] FotoPortada { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso {
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    //FotoPortada = request.FotoPortada
                };
                _context.Curso.Add(curso);
                var valor = await _context.SaveChangesAsync();

                if(valor > 0) return Unit.Value;

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}