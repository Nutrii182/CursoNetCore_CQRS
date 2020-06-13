using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RoleEliminarUsuario
    {
        public class Elimina : IRequest{
            public string Username { get; set; }
            public string RoleNombre { get; set; }
        }

        public class EliminaValidator : AbstractValidator<Elimina>{
            public EliminaValidator(){
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.RoleNombre).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Elimina>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(UserManager<Usuario> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.RoleNombre);

                if(role == null)
                    throw new HandlerException(HttpStatusCode.NotFound, new {mensaje = "No se encontro el role"});
                
                var user = await _userManager.FindByNameAsync(request.Username);

                if(user == null)
                    throw new HandlerException(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});

                var result = await _userManager.RemoveFromRoleAsync(user, request.RoleNombre);

                if(result.Succeeded)
                    return Unit.Value;
                
                throw new Exception("No su pudo eliminar el role del usuario");
            }
        }
    }
}