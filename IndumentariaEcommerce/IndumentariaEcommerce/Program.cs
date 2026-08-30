using IndumentariaEcommerce.Components;
using IndumentariaEcommerce.Data;
using IndumentariaEcommerce.Services;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Cultura es-AR: sin esto los precios se formatean con la cultura del
// servidor y se muestran como 24,900 en lugar de 24.900.
var cultura = new CultureInfo("es-AR");
CultureInfo.DefaultThreadCurrentCulture = cultura;
CultureInfo.DefaultThreadCurrentUICulture = cultura;

// ---------- Servicios ----------
// Todo lo que se registre acá tiene que ir antes de builder.Build().

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// EF Core sobre SQLite.
// Se registra una FACTORY y no el DbContext directo: en Blazor Server un
// servicio Scoped vive todo el circuito del usuario (minutos u horas) y el
// DbContext no es thread-safe, así que dos eventos solapados romperían un
// contexto compartido. La factory entrega un contexto nuevo y de vida corta
// para cada consulta.
builder.Services.AddDbContextFactory<AppDbContext>(opciones =>
    opciones.UseSqlite(builder.Configuration.GetConnectionString("ConexionSqlite")));

// Consultas del catálogo. Scoped es el default seguro: hoy no guarda estado
// propio, pero si mañana lo hiciera, Singleton lo compartiría entre usuarios.
builder.Services.AddScoped<ProductoService>();

var app = builder.Build();

// Crea la base si no existe y aplica las migraciones pendientes al arrancar.
// En un sistema en producción las migraciones se aplican durante el despliegue
// y no en el arranque de la aplicación; acá se hace así para que el proyecto
// quede funcionando con sólo clonarlo y ejecutarlo.
using (var contexto = app.Services
    .GetRequiredService<IDbContextFactory<AppDbContext>>()
    .CreateDbContext())
{
    contexto.Database.Migrate();
}

// ---------- Pipeline HTTP ----------
// El orden de los middlewares importa: cada request los atraviesa de arriba
// hacia abajo.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // HSTS obliga al navegador a usar HTTPS. 30 días por defecto.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
