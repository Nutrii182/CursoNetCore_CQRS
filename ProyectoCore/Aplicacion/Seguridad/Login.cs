using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            public Handler(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);

                if(usuario == null){
                    throw new HandlerException(HttpStatusCode.Unauthorized);
                }

                var result = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);

                if(result.Succeeded){
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = "Data del Token",
                        Username = usuario.UserName,
                        Email = usuario.Email,
                        Imagen = null
                    };
                }
                throw new HandlerException(HttpStatusCode.Unauthorized);
            }
        }
    }
}