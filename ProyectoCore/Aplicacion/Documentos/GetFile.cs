using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Documentos
{
    public class GetFile
    {
        public class Ejecuta : IRequest<GenericFile>{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta, GenericFile>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context){
                _context = context;
            }
            public async Task<GenericFile> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var file = await _context.Documento.Where(x => x.ObjetoReferencia == request.Id).FirstAsync();

                if(file == null){
                    throw new HandlerException(HttpStatusCode.NotFound, new { mensaje = "No se encontro la imagen"});
                }

                var genericFile = new GenericFile {
                    Data = Convert.ToBase64String(file.Contenido),
                    Nombre = file.Nombre,
                    Extension = file.Extension
                };

                return genericFile;
            }
        }
    }
}