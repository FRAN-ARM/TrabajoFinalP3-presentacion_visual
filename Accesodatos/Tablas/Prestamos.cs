using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Prestamos
    {
        public Guid id { get; set; }
        public DateTime fecha_prestamo { get; set; }
        public DateTime fecha_entrega { get; set; }
        public string estado { get; set; }

        [ForeignKey("cliente_id")]
        public Clientes Cliente { get; set; }
        public Guid cliente_id { get; set; }


        [ForeignKey("libro_id")]
        public Libros Libro { get; set; }
        public Guid libro_id { get; set; }





    }
}
