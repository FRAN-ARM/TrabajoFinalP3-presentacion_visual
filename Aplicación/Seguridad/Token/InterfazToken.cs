using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accesodatos.Tablas;

namespace Aplicación.Seguridad.Token
{
    public interface InterfazToken
    {
        string CrearToken(Usuario usuario);
    }
}
