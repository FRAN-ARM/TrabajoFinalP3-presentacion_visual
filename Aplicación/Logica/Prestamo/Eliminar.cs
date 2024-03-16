using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Prestamo
{
    public class Eliminar
    {
        public class EliminarPrestamo: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<EliminarPrestamo, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarPrestamo request, CancellationToken cancellationToken)
            {
                var prestamo = await _context.Prestamos.FindAsync(request.Id);
                if(prestamo == null)
                {
                    throw new Exception("No se encontro el prestamo");
                }
                _context.Remove(prestamo);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el prestamo");
            }
        }
    }
}
