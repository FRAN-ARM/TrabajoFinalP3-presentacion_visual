using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accesodatos.Tablas
{
    public class Autores
    {
        public Guid id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string pais { get; set; }
        public string descripcion { get; set; }
    }
}
