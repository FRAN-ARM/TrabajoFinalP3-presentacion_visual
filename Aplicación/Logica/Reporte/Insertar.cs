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

namespace Aplicación.Logica.Reporte
{
    public class Insertar
    {
        public class EjecutaReporte: IRequest<Unit>
        {
            public DateTime fecha_prestamo { get; set; }
            public Guid cliente_id { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaReporte>
        {
            public Validador()
            {
                RuleFor(x => x.fecha_prestamo).NotEmpty();
                RuleFor(x => x.cliente_id).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EjecutaReporte, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EjecutaReporte request, CancellationToken cancellationToken)
            {
                var reporte = new Reportes
                {
                    id = Guid.NewGuid(),
                    fecha_prestamo = request.fecha_prestamo,
                    cliente_id = request.cliente_id
                };
                _context.Reportes.Add(reporte);
                var resultados = await _context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el reporte");
            }
        }
    }
}
