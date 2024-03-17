using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Autor;

namespace SGBL.Controllers
{
    public class AutoresController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaAutor datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Autores>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaAutores());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EjecutaEliminar { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarAutor data )
        {
            data.id = id;
            return await Mediator.Send(data);
        }
    }
}
