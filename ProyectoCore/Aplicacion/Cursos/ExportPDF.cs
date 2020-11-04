using System.IO;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ExportPDF
    {
        public class Consulta : IRequest<Stream> {}

        public class Handler : IRequestHandler<Consulta, Stream>
        {
            private readonly CursosOnlineContext _context;
            public Handler(CursosOnlineContext context)
            {
                _context = context;
            }

            public async Task<Stream> Handle(Consulta request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Curso.ToListAsync();

                MemoryStream workStream = new MemoryStream();
                Rectangle rect = new Rectangle(PageSize.A4);

                Document document = new Document(rect, 0, 0, 50, 100);
                PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                writer.CloseStream = false;

                document.Open();
                document.AddTitle("Lista de Cursos Online");
                document.Add(new Phrase("Lista Cursos"));
                document.Close();

                byte[] byteData = workStream.ToArray();
                workStream.Write(byteData, 0, byteData.Length);
                workStream.Position = 0;

                return workStream; 
            }
        }
    }
}