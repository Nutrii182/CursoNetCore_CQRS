using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.Cursos;
using AutoMapper;
using Dominio;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistencia;
using Persistencia.DapperConnection;
using Persistencia.DapperConnection.Instructor;
using Persistencia.DapperConnection.Paginacion;
using Seguridad;
using Seguridad.TokenSegurity;
using WebAPI.Middleware;

namespace WebAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration) 
        {
            this.Configuration = configuration;
               
        }
                public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("corsApp", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<CursosOnlineContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddOptions();
            services.Configure<ConnectionConfig>(Configuration.GetSection("ConnectionStrings"));

            services.AddMediatR(typeof(Consulta.Handler).Assembly);

            services.AddControllers( opt => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
            var builder = services.AddIdentityCore<Usuario>();
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            identityBuilder.AddRoles<IdentityRole>();
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

            identityBuilder.AddEntityFrameworkStores<CursosOnlineContext>();
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();
            services.TryAddSingleton<ISystemClock, SystemClock>();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi Palabra Secreta"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt => {
                opt.TokenValidationParameters = new TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();
            services.AddAutoMapper(typeof(Consulta.Handler));

            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<IInstructor, InstructorRepository>();
            services.AddScoped<IPaginacion, PaginacionRepositorio>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo{
                    Title = "Servicios para mantenimiento de cursos",
                    Version = "v1"
                });
                c.CustomSchemaIds(c => c.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");

            app.UseMiddleware<HandlerErrorMiddleware>();
            
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();  
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cursos Online v1");
            });
        }
    }
}
