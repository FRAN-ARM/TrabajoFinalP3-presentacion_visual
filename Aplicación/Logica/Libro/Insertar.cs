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

namespace Aplicación.Logica.Libro
{
    public class Insertar
    {
        public class EjecutaLibro: IRequest<Unit>
        {
            public string nombre { get; set; }
            public int paginas { get; set; }
            public string descripcion { get; set; }
            public int edicion { get; set; }
            public DateTime fecha_publicacion { get; set; }
            public Guid autor_id { get; set; }
            public Guid categoria_id { get; set; }
            public Guid editorial_id { get; set; }
        }

        public class Validador: AbstractValidator<EjecutaLibro>
        {
            public Validador()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.paginas).NotEmpty();
                RuleFor(x => x.descripcion).NotEmpty();
                RuleFor(x => x.edicion).NotEmpty();
                RuleFor(x => x.fecha_publicacion).NotEmpty();
                RuleFor(x => x.autor_id).NotEmpty();
                RuleFor(x => x.categoria_id).NotEmpty();
                RuleFor(x => x.editorial_id).NotEmpty();
                
            }
        }

        public class Manejador : IRequestHandler<EjecutaLibro, Unit>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EjecutaLibro request, CancellationToken cancellationToken)
            {
                var parametroNombre = new SqlParameter("@nombre", request.nombre);
                var parametroPaginas = new SqlParameter("@paginas", request.paginas);
                var parametroDescripcion = new SqlParameter("@descripcion", request.descripcion);
                var parametroEdicion = new SqlParameter("@edicion", request.edicion);
                var parametrofecha_publicacion = new SqlParameter("@fecha_publicacion", request.fecha_publicacion);
                var parametroAutorId = new SqlParameter("@autor_id", request.autor_id);
                var parametroCategoria_id = new SqlParameter("@categoria_id", request.categoria_id);
                var parametroEditorial_id = new SqlParameter("@editorial_id", request.editorial_id);

                var resultado = await _context.Database.ExecuteSqlRawAsync("EXEC spLibroCrear @nombre, @paginas, @descripcion, @edicion,@fecha_publicacion, @autor_id, @categoria_id, @editorial_id", parametroNombre,parametroPaginas, parametroDescripcion, parametroEdicion, parametrofecha_publicacion, parametroAutorId, parametroCategoria_id, parametroEditorial_id  );

                if (resultado > 0)
                {
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el libro");
            }
        }
    }
}
