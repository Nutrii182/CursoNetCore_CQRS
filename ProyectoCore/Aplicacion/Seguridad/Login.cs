using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.HandlerError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class EjecutaValidation : AbstractValidator<Ejecuta>
        {
            public EjecutaValidation()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly CursosOnlineContext _context;
            public Handler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerator jwtGenerator, CursosOnlineContext context)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if(usuario == null){
                    throw new HandlerException(HttpStatusCode.Unauthorized);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                var roles = await _userManager.GetRolesAsync(usuario);
                var listRoles = new List<string>(roles);
                
                var imagenPerfil = await _context.Documento.Where(x => x.ObjetoReferencia == new Guid(usuario.Id)).FirstOrDefaultAsync();
                
                if(result.Succeeded){
                    if(imagenPerfil != null){
                        var imagenCliente = new ImagenGeneral {
                            Data = Convert.ToBase64String(imagenPerfil.Contenido),
                            Extension = imagenPerfil.Extension,
                            Nombre = imagenPerfil.Nombre
                        };
                        return new UsuarioData{
                            NombreCompleto = usuario.NombreCompleto,
                            Token = _jwtGenerator.CrearToken(usuario, listRoles),
                            Username = usuario.UserName,
                            Email = usuario.Email,
                            ImagenPerfil = imagenCliente
                        };

                    } else {
                        return new UsuarioData{
                            NombreCompleto = usuario.NombreCompleto,
                            Token = _jwtGenerator.CrearToken(usuario, listRoles),
                            Username = usuario.UserName,
                            Email = usuario.Email,
                        };
                    }
                }
                throw new HandlerException(HttpStatusCode.Unauthorized);
            }
        }
    }
}