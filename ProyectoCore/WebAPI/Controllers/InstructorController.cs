using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController : MyControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> GetInstructores(){
            return await Mediator.Send(new Consulta.Lista());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateInstructor(Nuevo.Ejecuta data){
            return await Mediator.Send(data);
        }
    }
}