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
    public class RoleEliminar
    {
        public class Elimina : IRequest{
            public string Nombre { get; set; }
        }

        public class EliminaValida : AbstractValidator<Elimina>{
            public EliminaValida(){
                RuleFor(x => x.Nombre).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Elimina>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager)
            {
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Elimina request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);

                if(role == null)
                    throw new HandlerException(HttpStatusCode.BadRequest, new {mensaje = "No se encontro el role"});

                var result = await _roleManager.DeleteAsync(role);

                if(result.Succeeded)
                    return Unit.Value;
                
                throw new Exception("No se pudo eliminar");
            }
        }
    }
}