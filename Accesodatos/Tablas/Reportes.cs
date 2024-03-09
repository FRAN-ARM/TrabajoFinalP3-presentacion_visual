using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Reportes
    {
        public Guid id { get; set; }
        public Guid usuario_id { get; set; }
        public DateTime fecha_prestamo { get; set; }
    }
}
