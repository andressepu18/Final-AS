Proyecto: Microservicios — Fasecolda y Validaciones

Descripción
- Conjunto de microservicios de ejemplo:
  - Fasecolda: expone accidentes por placa (API externa simulada).
  - Validaciones: consume Fasecolda, calcula puntos y persiste cotizaciones.

Puertos por defecto
- Fasecolda (MicroservicioFasecolda/CapaPresentacion): http://localhost:5001
- Validaciones (MicroservicioValidaciones/CapaPresentacion): http://localhost:5002

Prerequisitos
- .NET 10 SDK instalado
- SQL Server (SQLEXPRESS disponible en la máquina)
- Permisos para crear/leer las bases de datos y tablas indicadas

Connection strings (ejemplo en appsettings.json)
- Fasecolda (DBFasecolda):
  Server=DESKTOP-2UJFAS2\\SQLEXPRESS;Database=DBFasecolda;User Id=sa;Password=erlangshen56;TrustServerCertificate=True;
- Validaciones (DBValidaciones):
  Server=DESKTOP-2UJFAS2\\SQLEXPRESS;Database=DBValidaciones;User Id=sa;Password=erlangshen56;TrustServerCertificate=True;

Estructura de carpetas (relevante)
- MicroservicioFasecolda/CapaPresentacion  (API)
- MicroservicioFasecolda/CapaDatos        (acceso a DB)
- MicroservicioValidaciones/CapaPresentacion (API)
- MicroservicioValidaciones/CapaDatos    (persistencia)
- MicroservicioValidaciones/CapaDatosExternos (cliente HTTP hacia Fasecolda)
- MicroservicioValidaciones/CapaNegocio  (lógica de negocio)

Endpoints principales
- Fasecolda
  - GET /api/accidentes/{placa}
	- Devuelve lista de accidentes [{ idAccidente, placa, fecha, severidad }, ...]
- Validaciones
  - POST /api/validaciones
	- Body JSON: { "Placa": "ABC123", "CedulaCliente": "123" }
	- Respuesta: { cotizacion, placa, ccCliente, puntos, resultado }
  - GET /api/validaciones/puntos/{placa}
	- Devuelve { placa, puntos } (calculos en tiempo real, no persiste)
  - GET /api/validaciones/cotizacion/{placa}
	- Devuelve la última cotización persistida para la placa (si existe). Si la tabla está vacía se devuelve un DTO con valores por defecto (strings vacíos, puntos=0, cotizacion=false). Puede configurarse para devolver 404 si se prefiere.

Comandos básicos
- Restaurar paquetes:
  dotnet restore
- Compilar:
  dotnet build
- Ejecutar (desde la raíz del proyecto correspondiente):
  dotnet run --project <ruta-al-csproj>
  Ejemplo: dotnet run --project MicroservicioFasecolda/CapaPresentacion/Fasecolda.CapaPresentacion.csproj

Notas operativas
- Si la tabla de cotizaciones está vacía, la API /cotizacion/{placa} actualmente devuelve un objeto con campos por defecto. Cambiar a 404 es una opción válida si se prefiere.
- Evitar almacenar credenciales en appsettings.json en producción. Usar Secret Manager, variables de entorno o un vault.
- Si se realizan cambios en interfaces públicas (firmas de métodos), parar y reiniciar la aplicación para que los cambios se apliquen (Hot Reload puede no soportar ciertos cambios de firma).

Siguientes pasos sugeridos
- Añadir validación y manejo de errores (timeout/errores HTTP) en llamadas a Fasecolda.
- Añadir registros (ILogger) para diagnosticar llamadas fallidas y respuestas vacías.

Resumen
- Con los ajustes realizados los servicios se ejecutan en los puertos indicados y exponen los endpoints listados. Para pruebas con Postman arrancar ambos servicios y usar los endpoints descritos.
