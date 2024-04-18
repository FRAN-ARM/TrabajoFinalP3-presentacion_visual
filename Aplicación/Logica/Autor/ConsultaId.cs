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

namespace Aplicación.Logica.Autor
{
    public class ConsultaId
    {
        public class AutorDtoId
        {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string pais { get; set; }
            public string descripcion { get; set; }
        }

        public class AutorporId: IRequest<AutorDtoId>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<AutorporId, AutorDtoId>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<AutorDtoId> Handle(AutorporId request, CancellationToken cancellationToken)
            {
                var autor = await _context.Autores
                    .Where(A => A.id == request.Id)
                    .Select(A => new AutorDtoId
                    {
                        nombre = A.nombre,
                        apellido = A.apellido,
                        pais = A.pais,
                        descripcion = A.descripcion
                    }).FirstOrDefaultAsync();
                return autor;
            }
        }
    }
}
