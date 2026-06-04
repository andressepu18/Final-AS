using Microsoft.AspNetCore.Mvc;
using Validaciones.CapaDatos;
using Validaciones.CapaDatosExternos;
using Validaciones.CapaNegocio;
using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaPresentacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidacionesController : ControllerBase
    {
        private readonly Logica _logica;

        public ValidacionesController(Logica logica)
        {
            _logica = logica;
        }

        // POST api/validaciones
        [HttpPost]
        public IActionResult RealizarValidacion([FromBody] SolicitudDto solicitud)
        {
            CotizacionResultadoDto resultado = _logica.procesarSolicitudConResultado(solicitud);
            return Ok(new
            {
                cotizacion = resultado.Cotizacion,
                placa = resultado.Placa,
                ccCliente = resultado.CcCliente,
                puntos = resultado.Puntos,
                resultado = resultado.Resultado
            });
        }

        // GET api/validaciones/puntos/{placa} — devolver puntos calculados desde Fasecolda sin persistir
        [HttpGet("puntos/{placa}")]
        public IActionResult ConsultarPuntosPorPlaca(string placa)
        {
            int puntos = _logica.calcularPuntosPorPlaca(placa);
            return Ok(new { placa = placa, puntos = puntos });
        }

        // Alias: aceptar GET /api/validaciones/{placa} como atajo
        [HttpGet("{placa}")]
        public IActionResult ConsultarPuntosPorPlacaAlias(string placa)
        {
            int puntos = _logica.calcularPuntosPorPlaca(placa);
            return Ok(new { placa = placa, puntos = puntos });
        }

        // GET api/validaciones/cotizacion/{placa} — devolver última cotización almacenada
        [HttpGet("cotizacion/{placa}")]
        public IActionResult ObtenerCotizacionPorPlaca(string placa)
        {
            CotizacionResultadoDto cot = _logica.obtenerCotizacionPorPlaca(placa);
            return Ok(cot);
        }
    }
}
