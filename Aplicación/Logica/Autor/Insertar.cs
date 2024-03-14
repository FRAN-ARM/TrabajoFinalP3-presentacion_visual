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
    public class Insertar
    {
        public class EjecutaAutor: IRequest<Unit>
        {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string pais { get; set; }
            public string descripcion { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaAutor>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.apellido).NotEmpty();
                RuleFor(x => x.pais).NotEmpty();
                RuleFor(x => x.descripcion).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EjecutaAutor, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaAutor request, CancellationToken cancellationToken)
            {
                var parametroNombre = new SqlParameter("@nombre", request.nombre);
                var parametroApellido = new SqlParameter("@apellido", request.apellido);
                var parametroPais = new SqlParameter("@pais", request.pais);
                var parametroDescripcion = new SqlParameter("@descripcion", request.descripcion);

                var resultado = await _context.Database.ExecuteSqlRawAsync("EXEC spAutorCrear @nombre, @apellido, @pais, @descripcion", parametroNombre, parametroApellido, parametroPais, parametroDescripcion);

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el autor");
            }
        }
    }
}
