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
    public class ConsultaId
    {
       public class LibroDtoId
        {
            
            public string Nombre { get; set; }
            public int Paginas { get; set; }
            public string Descripcion { get; set; }
            public int Edicion { get; set; }
            public DateTime FechaPublicacion { get; set; }
            public Guid autor_id { get; set; }
            public Guid categoria_id { get; set; }
            public Guid editorial_id { get; set; }
        }

        public class LibroPorId : IRequest<LibroDtoId>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<LibroPorId, LibroDtoId>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<LibroDtoId> Handle(LibroPorId request, CancellationToken cancellationToken)
            {
                var libros = await _context.Libros
                .Where(l => l.id == request.Id)
                .Select( l => new LibroDtoId
                {
                    Nombre = l.nombre,
                    Descripcion = l.descripcion,
                    Edicion = l.edicion,
                    Paginas = l.paginas,
                    FechaPublicacion = l.fecha_publicacion,
                    autor_id = l.autor_id,
                    categoria_id = l.categoria_id,
                    editorial_id = l.editorial_id
                }).FirstOrDefaultAsync();
                return libros;
            }
        }
    }


}
