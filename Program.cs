using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FunerariaWeb; // Asegúrate que este sea el nombre de tu proyecto
using MudBlazor.Services; // <--- NECESARIO PARA MUDBLAZOR
using Blazored.LocalStorage; // <--- NECESARIO PARA EL TOKEN
using Microsoft.AspNetCore.Components.Authorization; // <--- NECESARIO PARA SEGURIDAD
using Funeraria.Web.Auth; // <--- DONDE CREASTE EL CustomAuthProvider

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. CONFIGURACIÓN DE LA API (HTTP)
// Asegúrate de que este puerto (7051) sea el mismo que usa tu API al correr
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7051") });

// 2. SERVICIOS DE MUDBLAZOR (¡Si falta esto, la web se queda cargando!)
builder.Services.AddMudServices();

// 3. MEMORIA LOCAL (LocalStorage)
builder.Services.AddBlazoredLocalStorage();

// 4. SISTEMA DE SEGURIDAD (AUTH)
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

await builder.Build().RunAsync();