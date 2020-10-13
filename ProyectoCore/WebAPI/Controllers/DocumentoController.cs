using System;
using System.Threading.Tasks;
using Aplicacion.Documentos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class DocumentoController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> SubirArchivo(UpFile.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenericFile>> ObtenerArchivo(Guid id){
            return await Mediator.Send(new GetFile.Ejecuta { Id = id });
        }
    }
}