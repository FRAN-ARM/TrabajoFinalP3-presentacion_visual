using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Libros
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public Guid autor_id { get; set; }
        public Guid categoria_id { get; set; }
    }
}
