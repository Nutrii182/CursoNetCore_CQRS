using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MyControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros){
            return await Mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta user){
            return await Mediator.Send(user);
        }

        [HttpGet]
        public async Task<ActionResult<UsuarioData>> ObtieneUsuario(){
            return await Mediator.Send(new UsuarioActual.Ejecutar());
        }
    }
}