using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.HandlerError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RoleNuevo
    {
        public class Ejecuta : IRequest{
            public string Nombre { get; set; }
        }

        public class ValidaEjecuta : AbstractValidator<Ejecuta>{
            public ValidaEjecuta(){
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Ejecuta>
        {
            private RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);

                if(role != null)
                    throw new HandlerException(HttpStatusCode.BadRequest, new {mensaje = "Ya existe el role"});

                var result = await _roleManager.CreateAsync(new IdentityRole(request.Nombre));

                if(result.Succeeded)
                    return Unit.Value;
                
                throw new Exception("No se pudo crear el role");
            }
        }
    }
}