using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia
{
    public class DataPrueba
    {
        public async static Task InsertData(CursosOnlineContext context, UserManager<Usuario> userManager){
            if(!userManager.Users.Any()){
                var user = new Usuario{NombreCompleto = "Martin Ruiz", UserName="Nutrii182", Email="nutrii182@gmail.com"};
                await userManager.CreateAsync(user, "Password123$");
            }
        }

    }
}