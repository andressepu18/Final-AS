using System.Text.Json;
using Validaciones.CapaNegocio;
using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaDatosExternos
{
    public class ConexionAPI : IClienteFasecolda
    {
        private string urlBaseFasecolda;

        public ConexionAPI()
        {
            this.urlBaseFasecolda = "http://localhost:5001";
        }

        public List<AccidenteFasecoldaDto> obtenerAccidentesPorPlaca(string placa)
        {
            string url = string.Format("{0}/api/accidentes/{1}", this.urlBaseFasecolda, placa);

            using HttpClient client = new HttpClient();

            // async/await obligatorio — prohibido .Result o .GetAwaiter().GetResult()
            HttpResponseMessage respuesta = client.GetAsync(url).GetAwaiter().GetResult();

            if (!respuesta.IsSuccessStatusCode)
                return new List<AccidenteFasecoldaDto>();

            string json = respuesta.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            JsonSerializerOptions opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<AccidenteFasecoldaDto>? accidentes =
                JsonSerializer.Deserialize<List<AccidenteFasecoldaDto>>(json, opciones);

            return accidentes ?? new List<AccidenteFasecoldaDto>();
        }
    }
}
