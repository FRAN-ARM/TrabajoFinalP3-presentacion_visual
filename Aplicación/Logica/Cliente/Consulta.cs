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
    public class Consulta
    {
        public class ListaCliente: IRequest<List<Clientes>> { }
        public class Manejador : IRequestHandler<ListaCliente, List<Clientes>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<List<Clientes>> Handle(ListaCliente request, CancellationToken cancellationToken)
            {
                var clientes = await _context.Clientes.ToListAsync();
                return clientes;
            }
        }
    }
}
