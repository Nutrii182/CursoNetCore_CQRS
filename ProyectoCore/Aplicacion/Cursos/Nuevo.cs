using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta : IRequest{
            public Guid? CursoId { get; set; }
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime? FechaPublicacion {get; set;}
            public byte[] FotoPortada { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public decimal Precio { get; set; }
            public decimal Promocion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();         
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
                Guid cursoId = Guid.NewGuid();

                if(request.CursoId != null){
                    cursoId = request.CursoId ?? Guid.NewGuid;
                }

                var curso = new Curso {
                    CursoId = cursoId,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaPublicacion = request.FechaPublicacion,
                    FechaCreacion = DateTime.UtcNow
                    //FotoPortada = request.FotoPortada
                };
                _context.Curso.Add(curso);

                if(request.ListaInstructor != null){
                    foreach(var id in request.ListaInstructor){
                        var cursoInstructor = new CursoInstructor{
                            CursoId = cursoId,
                            InstructorId = id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

                var precioEntidad = new Precio{
                    CursoId = cursoId,
                    PrecioActual = request.Precio,
                    Promocion = request.Promocion,
                    PrecioId = Guid.NewGuid()
                };

                _context.Precio.Add(precioEntidad);
                
                var valor = await _context.SaveChangesAsync();

                if(valor > 0) return Unit.Value;

                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}