using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
    public class Editar
    {
        public class Ejecuta : IRequest{
            public Guid InstructorId { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Titulo { get; set; }
        }

        public class EjecutaValida : AbstractValidator<Ejecuta>{
            public EjecutaValida(){
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Titulo).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructorRepository.UpdateInstruc(request.InstructorId, request.Nombre, request.Apellidos, request.Titulo);
                
                if(result > 0)
                    return Unit.Value;
                
                throw new Exception("No se pudo Actualizar");
            }
        }
    }
}