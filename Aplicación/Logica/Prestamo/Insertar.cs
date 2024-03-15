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
    public class Insertar
    {
        public class EjecutaPrestamo: IRequest<Unit>
        {
            public DateTime fecha_prestamo { get; set; }
            public DateTime fecha_entrega { get; set; }
            public string estado { get; set; }
            public Guid cliente_id { get; set; }
            public Guid libro_id { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaPrestamo>
        {
            public Validador()
            {
                RuleFor(x => x.fecha_prestamo).NotEmpty();
                RuleFor(x => x.fecha_entrega).NotEmpty();
                RuleFor(x => x.estado).NotEmpty();
                RuleFor(x => x.cliente_id).NotEmpty();
                RuleFor(x => x.libro_id).NotEmpty();
               
            }
        }

        public class Manejador : IRequestHandler<EjecutaPrestamo, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EjecutaPrestamo request, CancellationToken cancellationToken)
            {
                var prestamo = new Prestamos
                {
                    id = Guid.NewGuid(),
                    fecha_prestamo = request.fecha_prestamo,
                    fecha_entrega = request.fecha_entrega,
                    estado = request.estado,
                    cliente_id = request.cliente_id,
                    libro_id = request.libro_id
                };
                _context.Prestamos.Add(prestamo);

                var resultados = await _context.SaveChangesAsync();
                if (resultados > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el prestamo");
            }
        }
    }
}
