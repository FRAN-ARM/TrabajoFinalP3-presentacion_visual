using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Prestamos
    {
        public Guid id { get; set; }
        public Guid usuario_id { get; set; }
        public Guid libro_id { get; set; }
        public DateTime fecha_prestamo { get; set; }
        public string estado { get; set; }




    }
}
