using Microsoft.EntityFrameworkCore;
using Accesodatos.Context;
using Aplicación.Logica.Categoria;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Accesodatos.Tablas;
using Aplicación.Seguridad;
using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Aplicación.Seguridad.Token;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProyectoContext>(options =>
{
    options.UseSqlServer("name=DefaultConnection");
});
builder.Services.AddMediatR(typeof(Consulta.Manejador).Assembly);
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Insertar>());

var UserBuilder = builder.Services.AddIdentityCore<Usuario>();
var identityBuilder = new IdentityBuilder(UserBuilder.UserType, UserBuilder.Services);
builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

identityBuilder.AddEntityFrameworkStores<ProyectoContext>();
identityBuilder.AddSignInManager<SignInManager<Usuario>>();
builder.Services.AddCors(o => o.AddPolicy("corsApp", builder => {
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

// Add authentication with JWT bearer tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("palabra secreta")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("corsApp");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProyectoContext>();
    var userManager = services.GetRequiredService<UserManager<Usuario>>();
    /*var dataPrueba = new DataPrueba();
    await dataPrueba.InsertarUsuario(context, userManager);*/
}

app.Run();

app.Run();
