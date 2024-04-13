using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TrabajoFinalP3_presentacion_visual;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<Prestamos>();
builder.Services.AddScoped<Autores>();
builder.Services.AddScoped<Categorias>();
builder.Services.AddScoped<Clientes>();
builder.Services.AddScoped<Editorial>();
builder.Services.AddScoped<Libros>();
builder.Services.AddScoped<Usuario>();
builder.Services.AddScoped<Reportes>();
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
