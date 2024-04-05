using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Accesodatos.Context;
using Accesodatos.Tablas;
using Aplicación.Logica.Autor;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TestProject
{
    public class AutorTest
    {
        private ProyectoContext _context;
        private IMediator _mediator;

        public AutorTest()
        {
            var options = new DbContextOptionsBuilder<ProyectoContext>().UseInMemoryDatabase(databaseName: "TestDatabase")
                 .Options;
            _context = new ProyectoContext(options);

            _mediator = new Mediator((t) => { return null; });
        }

        [Fact]
        public async Task TestInsertarAutor()
        {
            
            var request = new Insertar.EjecutaAutor
            {

                nombre = "Horacio",
                apellido = "Quiroga",
                pais = "Uruguay",
                descripcion = "Fue un cuentista, dramaturgo y poeta uruguayo."
            };

            var handler = new Insertar.Manejador(_context);

          
            await handler.Handle(request, new System.Threading.CancellationToken());

            
            var autorInsertado = _context.Autores.FirstOrDefault(a => a.nombre == "Horacio");
            Assert.NotNull(autorInsertado);
        }

        [Fact]
        public async Task TestConsultarAutores()
        {
            // Arrange
            var consulta = new Consulta.ListaAutores();
            var handler = new Consulta.Manejador(_context);

            // Act
            var autores = await handler.Handle(consulta, new CancellationToken());

            // Assert
            Assert.NotNull(autores);
            Assert.NotEmpty(autores);
        }

        [Fact]
        public async Task TestEditarAutor()
        {
            // Arrange
            var autor = new Autores
            {
                nombre = "Leopoldo",
                apellido = "L",
                pais = "Argentina",
                descripcion = " fue un escritor modernista y polímata argentino. "
            };
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            var editarRequest = new Editar.EditarAutor
            {
                id = autor.id,
                nombre = "Leopoldo",
                apellido = "Lugones",
                pais = "Argentina",
                descripcion = "fue un escritor modernista y polímata argentino."
            };
            var editarHandler = new Editar.Manejador(_context);

            // Act
            await editarHandler.Handle(editarRequest, new CancellationToken());

            // Assert
            var autorEditado = await _context.Autores.FindAsync(autor.id);
            Assert.Equal("Leopoldo", autorEditado.nombre);
            Assert.Equal("Lugones", autorEditado.apellido);
            Assert.Equal("Argentina", autorEditado.pais);
            Assert.Equal("fue un escritor modernista y polímata argentino.", autorEditado.descripcion);
        }

        [Fact]
        public async Task TestEliminarAutor()
        {
            // Arrange
            var autor = new Autores
            {
                nombre = "Leopoldo",
                apellido = "Lugones",
                pais = "Argentina",
                descripcion = "fue un escritor modernista y polímata argentino."
            };
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();

            var eliminarRequest = new Eliminar.EjecutaEliminar { Id = autor.id };
            var eliminarHandler = new Eliminar.Manejador(_context);

            // Act
            await eliminarHandler.Handle(eliminarRequest, new CancellationToken());

            // Assert
            var autorEliminado = await _context.Autores.FindAsync(autor.id);
            Assert.Null(autorEliminado);
        }

    }
}
