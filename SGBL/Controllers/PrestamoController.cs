using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Prestamo;
using static Aplicación.Logica.Prestamo.Consulta;


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

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarPrestamo { Id = id });
        }
    }
}
