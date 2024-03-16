using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Categoria
{
    public class Eliminar
    {
        public class EliminarCategoria: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }

        public class Manejador : IRequestHandler<EliminarCategoria, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarCategoria request, CancellationToken cancellationToken)
            {
               var categoria = await _context.Categorias.FindAsync(request.Id); 
               if(categoria == null)
                {
                    throw new Exception("No se encontro la categoria");
                }
               _context.Categorias.Remove(categoria);
                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar la categoria");
            }
        }
    }
}
