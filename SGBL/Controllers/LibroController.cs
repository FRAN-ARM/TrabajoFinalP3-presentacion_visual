using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Libro;
using static Aplicación.Logica.Libro.Consulta;
using static Aplicación.Logica.Libro.ConsultaId;

namespace SGBL.Controllers
{
    public class LibroController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaLibro datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroDto>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaLibros());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibroDtoId>> ObtenerLibroPorId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.LibroPorId { Id = id });
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarLibro { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarLibro data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }
    }
}
