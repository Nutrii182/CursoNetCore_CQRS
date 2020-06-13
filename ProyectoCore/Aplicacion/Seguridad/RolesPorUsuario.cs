using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RolesPorUsuario
    {
        public class Ejecuta : IRequest<List<string>>{
            public string Username { get; set; }
        }

        public class Handler : IRequestHandler<Ejecuta, List<string>>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if(user == null)
                    throw new HandlerException(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});

                var result = await _userManager.GetRolesAsync(user);
                return result.ToList();
                // return new List<string>(result);
            }
        }
    }
}