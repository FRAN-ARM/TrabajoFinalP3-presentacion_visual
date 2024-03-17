using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Categoria;


namespace SGBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : GeneralController
    {

        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.Ejecuta datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Categorias>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaCategorias());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarCategoria { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarCategoria data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }
    }
}
