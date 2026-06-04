using Fasecolda.CapaNegocio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fasecolda.CapaPresentacion.Controllers
{
    [Route("api/accidentes")]
    [ApiController]
    public class AccidentesController : ControllerBase
    {
        private readonly Logica _logica;

        public AccidentesController(Logica logica)
        {
            _logica = logica;
        }

        [HttpGet("{placa}")]
        public async Task<IActionResult> ObtenerAccidentesPorPlaca(string placa)
        {
            var accidentes = await _logica.consultarAccidentesPorPlaca(placa);
            return Ok(accidentes);
        }
    }
}