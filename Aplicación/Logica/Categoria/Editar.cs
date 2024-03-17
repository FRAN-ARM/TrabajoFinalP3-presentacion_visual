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

namespace Aplicación.Logica.Categoria
{
    public class Editar
    {
        public class EditarCategoria: IRequest<Unit>
        {
            public Guid id { get; set; }
            public string nombre { get; set; }
        }

        public class Validador: AbstractValidator<EditarCategoria>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EditarCategoria, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarCategoria request, CancellationToken cancellationToken)
            {
                var categoria = await _context.Categorias.FindAsync(request.id);
                if(categoria == null)
                {
                    throw new Exception("No se encontro la categoria");
                }
                categoria.nombre = request.nombre ?? categoria.nombre;
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo editar la categoria");

            }
        }
    }
}
