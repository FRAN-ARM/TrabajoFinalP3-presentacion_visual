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
    public class Consulta
    {
        public class ListaAutores: IRequest<List<Autores>> { }
        public class Manejador : IRequestHandler<ListaAutores, List<Autores>>
        {
            private readonly ProyectoContext _context;
            public Manejador(ProyectoContext context)
            {
                _context = context;
            }
            public async Task<List<Autores>> Handle(ListaAutores request, CancellationToken cancellationToken)
            {
                var autores = await _context.Autores.ToListAsync();
                return autores;
            }
        }
    }
}
