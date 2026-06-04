namespace Validaciones.CapaNegocio.DTOs
{
    public class CotizacionResultadoDto
    {
        public bool Cotizacion { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string CcCliente { get; set; } = string.Empty;
        public int Puntos { get; set; }
        public string Resultado { get; set; } = string.Empty;
    }
}
