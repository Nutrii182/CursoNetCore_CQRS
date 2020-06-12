using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Paginacion;

namespace WebAPI.Controllers
{
    public class CursosController : MyControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<CursoDto>>> GetCursos(){
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CursoDto>> GetCursoId(Guid id){
            return await Mediator.Send(new ConsultaId.CursoUnico{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Modifica(Guid id, Editar.Ejecuta data){
            data.CursoId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Elimina(Guid id){
            return await Mediator.Send(new Eliminar.Elimina{Id = id});
        }

        [HttpPost("report")]
        public async Task<ActionResult<PaginacionModel>> Report(PaginacionCurso.Ejecuta data){
            return await Mediator.Send(data);
        }
    }
}