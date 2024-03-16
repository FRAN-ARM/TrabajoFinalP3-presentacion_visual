using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Reporte
{
    public class Eliminar
    {
        public class EliminarReporte: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<EliminarReporte, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarReporte request, CancellationToken cancellationToken)
            {
                var reporte = await _context.Reportes.FindAsync(request.Id);
                if(reporte == null)
                {
                    throw new Exception("No se pudo encontrar el reporte");
                }
                _context.Reportes.Remove(reporte);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el reporte");
            }
        }
    }
}
