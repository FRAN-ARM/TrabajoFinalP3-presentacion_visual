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

namespace Aplicación.Logica.Prestamo
{
    public class ConsultaId
    {
       public class PrestamoDtoId
        {
            public DateTime fecha_prestamo { get; set; }
            public DateTime fecha_entrega { get; set; }
            public string estado { get; set; }
            public Guid cliente_id { get; set; }
            public Guid libro_id { get; set; }
        }

        public class PrestamoId: IRequest<PrestamoDtoId>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<PrestamoId, PrestamoDtoId>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<PrestamoDtoId> Handle(PrestamoId request, CancellationToken cancellationToken)
            {
                var prestamo = await _context.Prestamos
                .Where(p => p.id == request.Id)
                .Select(p => new PrestamoDtoId
                {
                    fecha_prestamo = p.fecha_prestamo,
                    fecha_entrega = p.fecha_entrega,
                    estado = p.estado,
                    cliente_id = p.cliente_id,
                    libro_id = p.libro_id

                }).FirstOrDefaultAsync();

                return prestamo;
            }
        }
    }
}
