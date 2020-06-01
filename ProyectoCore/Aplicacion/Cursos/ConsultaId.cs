using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class ConsultaId
    {
        public class CursoUnico : IRequest<CursoDto>{
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<CursoUnico, CursoDto>
        {
            private readonly CursosOnlineContext _context;
            private readonly IMapper _mapper;
            public Handler(CursosOnlineContext context, IMapper mapper){
                _context = context;
                _mapper = mapper;
            }

            public async Task<CursoDto> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await _context.Curso
                .Include(x => x.ComentarioLista)
                .Include(x => x.PrecioPromocion)
                .Include(x => x.InstructoresLink)
                .ThenInclude(y => y.Instructor)
                .FirstOrDefaultAsync(c => c.CursoId == request.Id);

                if(curso == null) {
                    throw new HandlerException(HttpStatusCode.NotFound, new {message = "No se encontro el curso"});
                }

                var cursoDto = _mapper.Map<Curso, CursoDto>(curso);
                return cursoDto ;
            }
        }
    }
}