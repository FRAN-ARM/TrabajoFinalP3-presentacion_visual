using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accesodatos.Tablas;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Accesodatos.Context
{
    public class ProyectoContext : IdentityDbContext<Usuario>
    {
        public ProyectoContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Categorias> Categorias {get; set;}
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Editorial> Editorial { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<Reportes> Reportes { get; set; }
    }
}
