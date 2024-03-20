using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Accesodatos.Context;
using Accesodatos.Tablas;

namespace Aplicación.Seguridad
{
    public class DataPrueba
    {
        public async Task InsertarUsuario(ProyectoContext context, UserManager<Usuario> userManager)
        {
            if (!userManager.Users.Any())
            {
                var usuario = new Usuario
                {
                    nombre = "Luis",
                    apellido = "Lachapel",
                    UserName = "luislachapel",
                    Email = "luis@gmail"
                };
                await userManager.CreateAsync(usuario, "Contraseña123$");
            }
        }
    }
}
