using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia.DapperConnection.Instructor;

namespace Aplicacion.Instructores
{
    public class InstructorId
    {

        public class Ejecuta : IRequest<InstructorModel>{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta, InstructorModel>
        {
            private readonly IInstructor _instructorRepository;
            public Handler(IInstructor instructorRepository){
                _instructorRepository = instructorRepository;
            }
            public async Task<InstructorModel> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var result = await _instructorRepository.GetForId(request.Id);
                
                if(result == null)
                    throw new Exception("No se encontro el instructor");
                
                return result;
            }
        }
    }
}