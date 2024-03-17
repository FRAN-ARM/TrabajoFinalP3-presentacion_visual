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
    public class Editar
    {
        public class EditarPrestamo: IRequest<Unit>
        {
            public Guid id { get; set; }
            public DateTime? fecha_prestamo { get; set; }
            public DateTime? fecha_entrega { get; set; }
            public string? estado { get; set; }
            public Guid? cliente_id { get; set; }
            public Guid? libro_id { get; set; }
        }


        public class Manejador : IRequestHandler<EditarPrestamo, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarPrestamo request, CancellationToken cancellationToken)
            {
                var prestamo = await _context.Prestamos.FindAsync(request.id);
                if(prestamo == null)
                {
                    throw new Exception("No se encontro el prestamo");
                }

                prestamo.fecha_prestamo = request.fecha_prestamo ?? prestamo.fecha_prestamo;
                prestamo.fecha_entrega = request.fecha_entrega ?? prestamo.fecha_entrega;
                prestamo.estado = request.estado ?? prestamo.estado;
                prestamo.cliente_id = request.cliente_id ?? prestamo.cliente_id;
                prestamo.libro_id = request.libro_id ?? prestamo.libro_id;

                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("No se pudo editar el prestamo");

            }


        }
    }
}
