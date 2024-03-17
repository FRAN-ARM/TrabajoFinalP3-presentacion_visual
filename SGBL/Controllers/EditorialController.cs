using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Editora;

namespace SGBL.Controllers
{
    public class EditorialController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaEditorial datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Editorial>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaEditoriales());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarEditora { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarEditorial data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }
    }
}
