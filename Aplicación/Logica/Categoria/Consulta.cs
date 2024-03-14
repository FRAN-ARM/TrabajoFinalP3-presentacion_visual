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
    public class Consulta
    {
        public class ListaCategorias: IRequest<List<Categorias>> { }

        public class Manejador : IRequestHandler<ListaCategorias, List<Categorias>>
        {
           private readonly ProyectoContext _context;
           public Manejador(ProyectoContext context)
            {
                _context = context;
            }

            public async Task<List<Categorias>> Handle(ListaCategorias request, CancellationToken cancellationToken)
            {
                var categorias = await _context.Categorias.ToListAsync();
                return categorias;
            }
        }
    }
}
