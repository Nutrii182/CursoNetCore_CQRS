using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class RoleController : MyControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> CrearRole(RoleNuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> EliminarRole(RoleEliminar.Elimina data){
            return await Mediator.Send(data);
        }

        [HttpGet("lista")]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles(){
            return await Mediator.Send(new RoleLista.Ejecuta());
        }

        [HttpPost("agregarRoleUsuario")]
        public async Task<ActionResult<Unit>> AgregarRoleUsuario(RoleAgregarUsuario.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpPost("eliminarRoleUsuario")]
        public async Task<ActionResult<Unit>> ELiminarRoleUsuario(RoleEliminarUsuario.Elimina data){
            return await Mediator.Send(data);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<List<string>>> GetRolesUsuario(string username){
            return await Mediator.Send(new RolesPorUsuario.Ejecuta {Username = username});
        }
    }
}