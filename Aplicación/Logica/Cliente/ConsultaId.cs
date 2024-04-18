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
    public class ConsultaId
    {
      public class ClienteDtoId
      {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string correo { get; set; }
            public int matricula { get; set; }
            public DateTime? fecha_nacimiento { get; set; }
      }

        public class ClienteporId: IRequest<ClienteDtoId>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<ClienteporId, ClienteDtoId>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<ClienteDtoId> Handle(ClienteporId request, CancellationToken cancellationToken)
            {
                var cliente = await _context.Clientes
                .Where(c => c.id == request.Id)
                .Select(c => new ClienteDtoId
                {
                    nombre = c.nombre,
                    apellido = c.apellido,
                    correo = c.correo,
                    matricula = c.matricula,
                    fecha_nacimiento = c.fecha_nacimiento

                }).FirstOrDefaultAsync();

                return cliente;
            }
        }
    }
}
