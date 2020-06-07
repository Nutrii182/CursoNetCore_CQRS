using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using MediatR;
using Persistencia;

namespace Aplicacion.Comentarios
{
    public class Eliminar
    {
        public class Elimina : IRequest<Unit>{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Elimina>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var comment = await _context.Comentario.FindAsync(request.Id);

                if(comment == null)
                    throw new HandlerException(HttpStatusCode.NotFound, new {message = "No se encontro el comentario"});
                
                _context.Remove(comment);

                var result = await _context.SaveChangesAsync();

                if(result > 0)
                    return Unit.Value;

                throw new Exception("No se pudo eliminar el comentario");
            }
        }
    }
}