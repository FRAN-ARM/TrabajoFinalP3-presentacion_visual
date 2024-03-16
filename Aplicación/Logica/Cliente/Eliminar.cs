using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Cliente
{
    public class Eliminar
    {
        public class EliminarCliente: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<EliminarCliente, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarCliente request, CancellationToken cancellationToken)
            {
                var cliente = await _context.Clientes.FindAsync(request.Id);
                if(cliente == null)
                {
                    throw new Exception("No se encontro el cliente");
                }
                _context.Clientes.Remove(cliente);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar el cliente");
            }
        }
    }
}
