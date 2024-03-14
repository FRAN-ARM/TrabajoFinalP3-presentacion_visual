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
    public class Consulta
    {
        public class ListaEditoriales: IRequest<List<Editorial>> { }
        public class Manejador : IRequestHandler<ListaEditoriales, List<Editorial>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<List<Editorial>> Handle(ListaEditoriales request, CancellationToken cancellationToken)
            {
                var editoriales = await _context.Editorial.ToListAsync();
                return editoriales;
            }
        }
    }
}
