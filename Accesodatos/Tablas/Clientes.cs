using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Clientes
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public int matricula { get; set; }
        public string? foto_perfil { get; set; }
        public DateTime? fecha_nacimiento { get; set; }

    }
}
