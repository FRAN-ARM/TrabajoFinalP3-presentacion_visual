using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Accesodatos.Tablas;
using Accesodatos.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Aplicación.Logica.Libro
{
    public class Editar
    {
        public class EditarLibro: IRequest<Unit>
        {
            public Guid id { get; set; }
            public string? nombre { get; set; }
            public int? paginas { get; set; }
            public string? descripcion { get; set; }
            public int? edicion { get; set; }
            public DateTime? fecha_publicacion { get; set; }
            public Guid? autor_id { get; set; }
            public Guid? categoria_id { get; set; }
            public Guid? editorial_id { get; set; }
        }

        public class Manejador : IRequestHandler<EditarLibro, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarLibro request, CancellationToken cancellationToken)
            {
                var libro = await _context.Libros.FindAsync(request.id);
                if(libro == null)
                {
                    throw new Exception("No se pudo encontra el libro");

                }

                libro.nombre = request.nombre ?? libro.nombre;
                libro.paginas = request.paginas ?? libro.paginas;
                libro.descripcion = request.descripcion ?? libro.descripcion;
                libro.edicion = request.edicion ?? libro.edicion;
                libro.fecha_publicacion = request.fecha_publicacion ?? libro.fecha_publicacion;
                libro.autor_id = request.autor_id ?? libro.autor_id;
                libro.categoria_id = request.categoria_id ?? libro.categoria_id;
                libro.editorial_id = request.editorial_id ?? libro.editorial_id;

                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo realizar los cambios");

            }
        }
    }
}
