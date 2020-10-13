using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

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
            private readonly CursosOnlineContext _context;
            public Handler(UserManager<Usuario> userManager, IJwtGenerator jwtGenerator, IUsuarioSesion usuarioSesion, CursosOnlineContext context)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _usuarioSesion = usuarioSesion;
                _context = context;
            }

            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());

                var roles = await _userManager.GetRolesAsync(user);
                var listRoles = new List<string>(roles);

                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new System.Guid(user.Id)).FirstOrDefaultAsync();
                
                if(imagenPerfil != null){
                    var imagenCliente = new ImagenGeneral {
                        Data = Convert.ToBase64String(imagenPerfil.Contenido),
                        Extension = imagenPerfil.Extension,
                        Nombre = imagenPerfil.Nombre
                    };

                    return new UsuarioData{
                        NombreCompleto = user.NombreCompleto,
                        Username = user.UserName,
                        Token = _jwtGenerator.CrearToken(user, listRoles),
                        Email = user.Email,
                        ImagenPerfil = imagenCliente
                    };

                } else {
                    return new UsuarioData{
                        NombreCompleto = user.NombreCompleto,
                        Username = user.UserName,
                        Token = _jwtGenerator.CrearToken(user, listRoles),
                        Email = user.Email
                    };
                }
            }
        }
    }
}