using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
    public class Nuevo
    {
        public class Ejecuta : IRequest {

            public string Alumno { get; set; }
            public int Puntaje { get; set; }
            public string Comentario { get; set; }
            public Guid CursoId { get; set; }
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>{
            public EjecutaValida(){
                RuleFor(x => x.Alumno).NotEmpty();
                RuleFor(x => x.Comentario).NotEmpty();
                RuleFor(x => x.Puntaje).NotEmpty();
                RuleFor(x => x.CursoId).NotEmpty();
            }
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
                var comentario = new Comentario{
                    ComentarioId = Guid.NewGuid(),
                    Alumno = request.Alumno,
                    ComentarioTexto = request.Comentario,
                    Puntaje = request.Puntaje,
                    CursoId = request.CursoId
                };

                _context.Comentario.Add(comentario);
                var result = await _context.SaveChangesAsync();

                if(result > 0)
                    return Unit.Value;
                
                throw new Exception("No se pudo agregar el comentario");
            }
        }
    }
}