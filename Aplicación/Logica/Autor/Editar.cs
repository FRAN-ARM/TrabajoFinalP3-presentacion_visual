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
    public class Editar
    {
        public class EditarAutor: IRequest<Unit>
        {
            public Guid id { get; set; }
            public string? nombre { get; set; }
            public string? apellido { get; set; }
            public string? pais { get; set; }
            public string? descripcion { get; set; }
        }


        public class Manejador : IRequestHandler<EditarAutor, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(EditarAutor request, CancellationToken cancellationToken)
            {
                
                var autor = await _context.Autores.FindAsync(request.id);
                if(autor == null)
                {
                    throw new Exception("No se encontro el autor");
                }
                autor.nombre = request.nombre ?? autor.nombre;
                autor.apellido = request.apellido ?? autor.apellido;
                autor.pais = request.pais ??  autor.pais;
                autor.descripcion = request.descripcion ??  autor.descripcion;

                var resultado = await _context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo editar el autor");

            }
        }
    }
}
