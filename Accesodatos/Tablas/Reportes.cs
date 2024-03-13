using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accesodatos.Tablas
{
    public class Reportes
    {
        public Guid id { get; set; }
        public DateTime fecha_prestamo { get; set; }

        [ForeignKey("cliente_id")]
        public Clientes Cliente { get; set; }
        public Guid cliente_id { get; set; }
    }
}
