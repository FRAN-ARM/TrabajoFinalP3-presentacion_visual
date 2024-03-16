using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Libro
{
    public class Eliminar
    {
        public class EliminarLibro: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<EliminarLibro, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarLibro request, CancellationToken cancellationToken)
            {
                var libro = await _context.Libros.FindAsync(request.Id);
                if(libro == null)
                {
                    throw new Exception("No se encontro el libro");
                }
                _context.Libros.Remove(libro);
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el libro");

            }
        }
    }
}
