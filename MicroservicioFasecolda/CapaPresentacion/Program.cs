using Fasecolda.CapaNegocio;
using Fasecolda.CapaDatos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// ============================================================
// REGISTRO DE DEPENDENCIAS (Inversión de dependencias)
// ============================================================
// Registrar IRepositorioAccidentes pasando la connection string desde appsettings.json
builder.Services.AddScoped<IRepositorioAccidentes>(sp =>
    new ConexionSQL(sp.GetRequiredService<Microsoft.Extensions.Configuration.IConfiguration>().GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Logica>();
// ============================================================

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();