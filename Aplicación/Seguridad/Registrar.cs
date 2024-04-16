using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicación.Seguridad;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Accesodatos.Tablas;
using Accesodatos.Context;
using Microsoft.EntityFrameworkCore;

namespace Aplicación.Seguridad
{
    public class Registrar
    {
        public class Registro: IRequest<DataUsuario>
        {
            public string nombre { get; set; }
            public string apellido { get; set; }
            public string Email { get; set; }
            public string Contraseña { get; set; }
            public string Username { get; set; }
        }

        public class ValidacionRegistro: AbstractValidator<Registro>
        {
            public ValidacionRegistro()
            {
                RuleFor(x => x.nombre).NotEmpty();
                RuleFor(x => x.apellido).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Contraseña).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Registro, DataUsuario>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly ProyectoContext _context;
            public Manejador(UserManager<Usuario> userManager, ProyectoContext context)
            {
                _userManager = userManager;
                _context = context;
            }

            public async Task<DataUsuario> Handle(Registro request, CancellationToken cancellationToken)
            {
                var existeUsuario = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existeUsuario)
                {
                    throw new Exception("Este correo ya esta registrado");
                }

                var existeUsername = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUsername)
                {
                    throw new Exception("Este username ya existe");
                }

                var usuario = new Usuario
                {
                    nombre = request.nombre,
                    apellido = request.apellido,
                    Email = request.Email,
                    UserName = request.Username

                };
                var resultado = await _userManager.CreateAsync(usuario, request.Contraseña);
                if (resultado.Succeeded)
                {
                    return new DataUsuario
                    {
                        nombre = usuario.nombre,
                        apellido = usuario.apellido,
                        Email = usuario.Email,
                        Username = usuario.UserName
                        
                    };
                }
                else
                {

                    throw new Exception("No se puede agregar el usuario");
                }

            }
        }
    }
}
