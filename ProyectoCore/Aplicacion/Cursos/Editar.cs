using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta : IRequest{
            public Guid CursoId { get; set; }
            public string Titulo {get; set;}
            public string Descripcion {get; set;}
            public DateTime? FechaPublicacion {get; set;}
            //public byte[] FotoPortada { get; set; }
            public List<Guid> ListaInstructor { get; set; }
            public decimal? Precio { get; set; }
            public decimal? Promocion { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x => x.Titulo).NotEmpty();
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.FechaPublicacion).NotEmpty();         
            }
        }

        public class Handler : IRequestHandler<Ejecuta>{
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context){
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso.FindAsync(request.CursoId);

                if(curso == null) {
                    throw new HandlerException(HttpStatusCode.NotFound, new {message = "No se encontro el curso"});
                }
                
                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;

                var precioEntidad = _context.Precio.Where(x => x.CursoId == curso.CursoId).FirstOrDefault();

                if(precioEntidad != null){
                    precioEntidad.Promocion = request.Promocion ?? precioEntidad.Promocion;
                    precioEntidad.PrecioActual = request.Precio ?? precioEntidad.PrecioActual;
                } else {
                    precioEntidad = new Precio{
                        PrecioId = Guid.NewGuid(),
                        PrecioActual = request.Precio ?? 0,
                        Promocion = request.Promocion ?? 0,
                        CursoId = curso.CursoId
                    };
                    await _context.Precio.AddAsync(precioEntidad);
                }

                if(request.ListaInstructor != null){
                    if(request.ListaInstructor.Count > 0){
                        // Elimina instructores actuales
                        var instructoresDB = _context.CursoInstructor.Where(x => x.CursoId == request.CursoId);
                        foreach(var id in instructoresDB){
                            _context.CursoInstructor.Remove(id);
                        }
                        // Agregar instructores del cliente
                        foreach(var ids in request.ListaInstructor){
                            var newInstructor = new CursoInstructor{
                                CursoId = request.CursoId,
                                InstructorId = ids
                            };
                            _context.CursoInstructor.Add(newInstructor);
                        }
                    }
                }

                var valor = await _context.SaveChangesAsync();

                if(valor > 0) return Unit.Value;

                throw new Exception("No se pudo actualizar");
            }
        }
    }
}