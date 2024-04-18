using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Cliente;
using static Aplicación.Logica.Cliente.ConsultaId;

namespace SGBL.Controllers
{
    public class ClienteController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaCliente datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<Clientes>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaCliente());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDtoId>> ObtenerClienteporId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.ClienteporId { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarCliente{ Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarCliente data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }

    }
}
