using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Accesodatos.Tablas;
using Accesodatos.Context;

namespace Aplicación.Logica.Editora
{
    public class Eliminar
    {
        public class EliminarEditora: IRequest<Unit>
        {
            public Guid Id { get; set; }
        }
        public class Manejador : IRequestHandler<EliminarEditora, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EliminarEditora request, CancellationToken cancellationToken)
            {
                var editora = await _context.Editorial.FindAsync(request.Id);
                if(editora == null)
                {
                    throw new Exception("No se encontro la editora");
                }
                _context.Editorial.Remove(editora);
                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo eliminar la editora");
            }
        }
    }
}
