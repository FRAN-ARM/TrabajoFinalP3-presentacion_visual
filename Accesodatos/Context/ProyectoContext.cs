using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accesodatos.Tablas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Accesodatos.Context
{
    public class ProyectoContext : IdentityDbContext<Administrador>
    {

    }
}
