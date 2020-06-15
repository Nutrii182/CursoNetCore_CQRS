using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Instructores;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistencia.DapperConnection.Instructor;

namespace WebAPI.Controllers
{
    public class InstructorController : MyControllerBase
    {
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<InstructorModel>>> GetInstructores(){
            return await Mediator.Send(new Consulta.Lista());
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> CreateInstructor(Nuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> UpdateInstructor(Guid id, Editar.Ejecuta data){
            data.InstructorId = id;
            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> DeleteInstructor(Guid id){
            return await Mediator.Send(new Eliminar.Elimina{Id = id});
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstructorModel>> GetInstructor(Guid id){
            return await Mediator.Send(new InstructorId.Ejecuta{Id = id});
        }
    }
}