using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Autor
{
    public class Eliminar
    {
        public class EjecutaEliminar: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<EjecutaEliminar, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaEliminar request, CancellationToken cancellationToken)
            {
                var autor = await _context.Autores.FindAsync(request.Id);
                if(autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                _context.Remove(autor);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el autor");
            }
        }
    }
}
