namespace Validaciones.CapaNegocio.DTOs
{
    /// <summary>
    /// Datos que el cliente envía al endpoint POST /api/validaciones.
    /// </summary>
    public class SolicitudDto
    {
        public string CedulaCliente { get; set; } = string.Empty;
        public string Placa         { get; set; } = string.Empty;
    }
}
