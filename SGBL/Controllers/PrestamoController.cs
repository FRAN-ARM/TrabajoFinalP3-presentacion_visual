using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Prestamo;
using static Aplicación.Logica.Prestamo.Consulta;
using static Aplicación.Logica.Prestamo.ConsultaId;

namespace SGBL.Controllers
{
    public class PrestamoController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaPrestamo datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<PrestamoDto>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaPrestamos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PrestamoDtoId>> ObtenerPrestamoId(Guid id)
        {
            return await Mediator.Send(new ConsultaId.PrestamoId { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarPrestamo { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(Guid id, Editar.EditarPrestamo data)
        {
            data.id = id;
            return await Mediator.Send(data);
        }
    }
}
