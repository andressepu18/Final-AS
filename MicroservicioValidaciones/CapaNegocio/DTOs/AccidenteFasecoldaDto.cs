namespace Validaciones.CapaNegocio.DTOs
{
    /// <summary>
    /// Representa un accidente crudo tal como lo devuelve la API de Fasecolda.
    /// Propiedad por propiedad idéntica al JSON de respuesta de Fasecolda.
    /// </summary>
    public class AccidenteFasecoldaDto
    {
        public int    IdAccidente { get; set; }
        public string Placa       { get; set; } = string.Empty;
        public string Fecha       { get; set; } = string.Empty;
        public string Severidad   { get; set; } = string.Empty;
    }
}
