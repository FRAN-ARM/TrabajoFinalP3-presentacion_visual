using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Reporte;
using static Aplicación.Logica.Reporte.Consulta;

namespace SGBL.Controllers
{
    public class ReporteController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>>Insertar(Insertar.EjecutaReporte datos)
        {
            return await Mediator.Send(datos);
        }

        [HttpGet]
        public async Task<ActionResult<List<ReporteDto>>> Lista()
        {
            return await Mediator.Send(new Consulta.ListaReporte());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(Guid id)
        {
            return await Mediator.Send(new Eliminar.EliminarReporte { Id = id });
        }
    }
}
