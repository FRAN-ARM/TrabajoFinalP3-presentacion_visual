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

namespace Aplicación.Logica.Cliente
{
    public class Editar
    {
        public class EditarCliente: IRequest<Unit>
        {
            public Guid id { get; set; }
            public string? nombre { get; set; }
            public string? apellido { get; set; }
            public string correo { get; set; }

        }
        public class Manejador : IRequestHandler<EditarCliente, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarCliente request, CancellationToken cancellationToken)
            {
                var cliente = await _context.Clientes.FindAsync(request.id);
                if (cliente == null)
                {
                    throw new Exception("No se encontro el cliente");
                }

                    cliente.nombre = request.nombre ?? cliente.nombre;
                    cliente.apellido = request.apellido ?? cliente.apellido;
                    cliente.correo = request.correo ?? cliente.correo;
                


                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo editar el cliente");
            }
        }
    }

}
