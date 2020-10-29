using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Documentos
{
    public class UpFile
    {
        public class Ejecuta : IRequest {
            public Guid? ObjetoReferencia { get; set; }
            public string Data { get; set; }
            public string Nombre { get; set; }
            public string Extension { get; set; }
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
                var document = await _context.Documento.Where(x => x.ObjetoReferencia == request.ObjetoReferencia).FirstOrDefaultAsync();

                if(document == null){
                    var doc = new Documento {
                        Contenido = Convert.FromBase64String(request.Data),
                        Nombre = request.Nombre,
                        Extension = request.Extension,
                        DocumentoId = Guid.NewGuid(),
                        FechaCreacion = DateTime.UtcNow,
                        ObjetoReferencia = request.ObjetoReferencia ?? Guid.Empty
                    };
                    _context.Documento.Add(doc);
                } else {
                    document.Contenido = Convert.FromBase64String(request.Data);
                    document.Nombre = request.Nombre;
                    document.Extension = request.Extension;
                    document.FechaCreacion = DateTime.UtcNow;
                }

                var result = await _context.SaveChangesAsync();

                if(result > 0)
                    return Unit.Value;
                throw new Exception("No se pudo guardar el archivo");  
            }
        }
    }
}