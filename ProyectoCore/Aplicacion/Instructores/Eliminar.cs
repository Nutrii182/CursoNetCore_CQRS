using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
    public class Eliminar
    {
        public class Elimina : IRequest{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Elimina>
        {
            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository)
            {
                _instructorRepository = instructorRepository;
            }
            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var result = await _instructorRepository.DeleteInstruc(request.Id);

                if(result > 0)
                    return Unit.Value;
                
                throw new Exception("No se pudo eliminar");
            }
        }
    }
}