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
    public class Consulta
    {
        public class LibroDto
        {
            public Guid Id { get; set; }
            public string Nombre { get; set; }
            public int Paginas { get; set; }
            public string Descripcion { get; set; }
            public int Edicion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public string Autor { get; set; }
            public string Categoria { get; set; }
            public string Editorial { get; set; }
        }

        public class ListaLibros: IRequest<List<LibroDto>> {
            
        }
        public class Manejador : IRequestHandler<ListaLibros, List<LibroDto>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<List<LibroDto>> Handle(ListaLibros request, CancellationToken cancellationToken)
            {

                var libros = await _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.categoria)
                .Include(l => l.editorial)
                .Select(l => new LibroDto
                 {
                   Id = l.id,
                   Nombre = l.nombre,
                   Paginas = l.paginas,
                   Descripcion = l.descripcion,
                   Edicion = l.edicion,
                   FechaPublicacion = l.fecha_publicacion,
                   Autor = l.Autor.nombre + " " + l.Autor.apellido,
                   Categoria = l.categoria.nombre,
                   Editorial = l.editorial.nombre
                 }) .ToListAsync();


                return libros;

  
            }

        }
    }
}
