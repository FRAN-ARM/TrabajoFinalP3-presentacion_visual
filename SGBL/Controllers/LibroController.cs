using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Logica.Libro;
using static Aplicación.Logica.Libro.Consulta;

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

    }
}
