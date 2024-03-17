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

namespace Aplicación.Logica.Editora
{
    public class Editar
    {
        public class EditarEditorial: IRequest<Unit>
        {
            public Guid id { get; set; }
            public string nombre { get; set; }
        }
        public class Validador: AbstractValidator<EditarEditorial>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EditarEditorial, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarEditorial request, CancellationToken cancellationToken)
            {
                var editora = await _context.Editorial.FindAsync(request.id);
                if(editora == null)
                {
                    throw new Exception("No se encontro la editora");
                }
                editora.nombre = request.nombre ?? editora.nombre;
                var resultado = await _context.SaveChangesAsync();

                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo realizar los cambios");
            }
        }
    }
}
