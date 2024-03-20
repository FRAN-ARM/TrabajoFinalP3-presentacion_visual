using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Accesodatos.Tablas;

namespace Aplicación.Seguridad
{
    public class Login
    {
        public class EjecutaLogin: IRequest<DataUsuario>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


        public class Validador: AbstractValidator<EjecutaLogin>
        {
            public Validador()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<EjecutaLogin, DataUsuario>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            public async Task<DataUsuario> Handle(EjecutaLogin request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if(usuario == null)
                {
                    throw new Exception("No se encontro el usuario");
                }
                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario, request.Password, false);
                if (resultado.Succeeded)
                {
                    return new DataUsuario
                    {
                        nombre = usuario.nombre,
                        apellido = usuario.apellido,
                        Username = usuario.UserName,
                        Email = usuario.Email
                    };
                }
                throw new Exception("La contraseña es incorrecta");
               
            }
        }
    }
}
