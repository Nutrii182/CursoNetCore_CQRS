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
    public class UsuarioActualizar
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Username { get; set; }
        }

        public class EjecutaValidator : AbstractValidator<Ejecuta>{
            public EjecutaValidator(){
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellidos).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();           
            }
        }

        public class Handler : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IPasswordHasher<Usuario> _passwordHasher;

            public Handler(CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerator jwtGenerator, IPasswordHasher<Usuario> passwordHasher)
            {
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _passwordHasher = passwordHasher;
            }

            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.Username);

                if(user == null)
                    throw new HandlerException(HttpStatusCode.NotFound, new {mensaje = "El usuario no existe"});
                
                var userExist = await _context.Users.Where(x => x.Email == request.Email && x.UserName != request.Username).AnyAsync();

                if(userExist)
                    throw new HandlerException(HttpStatusCode.InternalServerError, new {mensaje = "El usuario y/o el correo ya existen"});
                
                user.NombreCompleto = request.Nombre + " " + request.Apellidos;
                user.Email = request.Email;
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

                var result = await _userManager.UpdateAsync(user);

                var roles = await _userManager.GetRolesAsync(user);
                var listRoles = new List<string>(roles);

                if(result.Succeeded){
                    return new UsuarioData{
                        NombreCompleto = user.NombreCompleto,
                        Username = user.UserName,
                        Email = user.Email,
                        Token = _jwtGenerator.CrearToken(user, listRoles)
                    };
                }
                
                throw new Exception("No se pudo actualizar el usuario");
            }
        }
    }
}