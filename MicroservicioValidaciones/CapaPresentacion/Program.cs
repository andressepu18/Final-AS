// ============================================================
// Validaciones.CapaPresentacion — Program.cs
// Puerto: http://localhost:5002
// ============================================================
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Registrar repositorio de validaciones con connection string desde appsettings.json
builder.Services.AddScoped<Validaciones.CapaNegocio.IRepositorioValidaciones>(sp =>
    new Validaciones.CapaDatos.ConexionSQL(sp.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>().GetConnectionString("DefaultConnection")));

// Registrar cliente externo (Fasecolda) y la capa de negocio para inyección
builder.Services.AddScoped<Validaciones.CapaNegocio.IClienteFasecolda, Validaciones.CapaDatosExternos.ConexionAPI>();
builder.Services.AddScoped<Validaciones.CapaNegocio.Logica>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
