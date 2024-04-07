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
    public class Consulta
    {
        public class ReporteDto
        {
            public Guid Id { get; set; }
            public DateTime fecha_prestamo { get; set; }
            public string cliente { get; set; }
        }

        public class ListaReporte: IRequest<List<ReporteDto>> { }

        public class Manejador : IRequestHandler<ListaReporte, List<ReporteDto>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<List<ReporteDto>> Handle(ListaReporte request, CancellationToken cancellationToken)
            {
                var reportes = await _context.Reportes
                .Include(r => r.Cliente)
                .Select(r => new ReporteDto
                {
                    Id = r.id,
                    fecha_prestamo = r.fecha_prestamo,
                    cliente = r.Cliente.nombre
                }).ToListAsync();

                return reportes;
            }
        }
    }
}
