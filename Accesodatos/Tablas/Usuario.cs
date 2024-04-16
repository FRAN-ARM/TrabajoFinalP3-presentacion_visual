using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Usuario : IdentityUser
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string? foto_perfil { get; set; }
    }
}
