using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData> {}

        public class Handler : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUsuarioSesion _usuarioSesion;
            public Handler(UserManager<Usuario> userManager, IJwtGenerator jwtGenerator, IUsuarioSesion usuarioSesion)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _usuarioSesion = usuarioSesion;
            }

            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

                var roles = await _userManager.GetRolesAsync(user);
                var listRoles = new List<string>(roles);

                return new UsuarioData{
                    NombreCompleto = user.NombreCompleto,
                    Username = user.UserName,
                    Token = _jwtGenerator.CrearToken(user, listRoles),
                    Imagen = null,
                    Email = user.Email
                };
            }
        }
    }
}