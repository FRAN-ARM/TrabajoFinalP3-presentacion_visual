using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Accesodatos.Tablas
{
    public class Libros
    {
        
        public Guid id { get; set; }
        public string nombre { get; set; }
        public int paginas { get; set; }
        public string descripcion { get; set; }
        public int edicion { get; set; }
        public DateTime fecha_publicacion { get; set; }
        public int stock { get; set; }
        public string? imagen { get; set; }

        [ForeignKey("autor_id")]
        public Autores Autor { get; set; }
        public Guid autor_id { get; set; }

        [ForeignKey("categoria_id")]
        public Categorias categoria { get; set; }
        public Guid categoria_id { get; set; }

        [ForeignKey("editorial_id")]
        public Editorial editorial { get; set; }
        public Guid editorial_id { get; set; }



    }
}
