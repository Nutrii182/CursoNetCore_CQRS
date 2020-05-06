using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostServer = CreateHostBuilder(args).Build();
            using(var environment = hostServer.Services.CreateScope()){
                var services = environment.ServiceProvider;

                try{
                    var context = services.GetRequiredService<CursosOnlineContext>();
                    context.Database.Migrate();
                }catch(Exception e){
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(e, "Ocurrió un error en la migración");
                }   
            }
            hostServer.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
