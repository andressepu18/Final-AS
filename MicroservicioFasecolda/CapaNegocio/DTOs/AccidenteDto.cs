namespace Fasecolda.CapaNegocio.DTOs
{
    /// <summary>
    /// Objeto de transferencia que representa un accidente crudo
    /// tal como se almacena en Db_Fasecolda.
    /// </summary>
    public class AccidenteDto
    {
        public int    IdAccidente { get; set; }
        public string Placa       { get; set; } = string.Empty;
        public string Fecha       { get; set; } = string.Empty;
        public string Severidad   { get; set; } = string.Empty;
    }
}
