using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Prestamo;

namespace SGBL.Controllers
{
    public class PrestamoController : GeneralController
    {
        [HttpPost]
        public async Task<ActionResult<Unit>> Insertar(Insertar.EjecutaPrestamo datos)
        {
            return await Mediator.Send(datos);
        }
    }
}
