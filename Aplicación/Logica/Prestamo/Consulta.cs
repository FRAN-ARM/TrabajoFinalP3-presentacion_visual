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
    public class Consulta
    {
        public class PrestamoDto
        {
            public Guid Id { get; set; }
            public DateTime fecha_prestamo { get; set; }
            public DateTime fecha_entrega { get; set; }
            public string estado { get; set; }
            public string cliente { get; set; }
            public string libro { get; set; }
        }

        public class ListaPrestamos: IRequest<List<PrestamoDto>> { }

        public class Manejador : IRequestHandler<ListaPrestamos, List<PrestamoDto>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<List<PrestamoDto>> Handle(ListaPrestamos request, CancellationToken cancellationToken)
            {
                var prestamos = await _context.Prestamos
                .Include(p => p.Cliente)
                .Include(p => p.Libro)
                .Select(p => new PrestamoDto
                {
                    Id = p.id,
                    fecha_prestamo = p.fecha_prestamo,
                    fecha_entrega = p.fecha_entrega,
                    estado = p.estado,
                    cliente = p.Cliente.nombre,
                    libro = p.Libro.nombre


                }).ToListAsync();

                return prestamos;
            }
        }
    }
}
