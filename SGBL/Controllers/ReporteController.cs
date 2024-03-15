using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Reporte;

namespace SGBL.Controllers
{
    public class ReporteController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>>Insertar(Insertar.EjecutaReporte datos)
        {
            return await Mediator.Send(datos);
        }
    }
}
