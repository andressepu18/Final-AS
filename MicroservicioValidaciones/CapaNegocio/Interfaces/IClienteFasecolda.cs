using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaNegocio
{
    // Contrato para ConexionAPI (hereda esto, igual que FuenteDatosExternos del ref)
    public interface IClienteFasecolda
    {
        List<AccidenteFasecoldaDto> obtenerAccidentesPorPlaca(string placa);
    }
}
