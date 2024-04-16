using Microsoft.AspNetCore.Mvc;
using Accesodatos.Context;
using Accesodatos.Tablas;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Aplicación.Seguridad;


namespace SGBL.Controllers
{
    public class UsuarioController : GeneralController
    {
        [HttpPost("login")]
        public async Task<ActionResult<DataUsuario>> Login(Login.EjecutaLogin parametros)
        {
            return await Mediator.Send(parametros);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<DataUsuario>>Registrar(Registrar.Registro parametros)
        {
            return await Mediator.Send(parametros);
        }
    }
}
