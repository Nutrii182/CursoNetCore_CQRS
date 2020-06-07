using System;
using System.Threading.Tasks;
using Aplicacion.Comentarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class ComentarioController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> CreateComentario(Nuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteComentario(Guid id){
            return await Mediator.Send(new Eliminar.Elimina{Id = id});
        }
    }
}