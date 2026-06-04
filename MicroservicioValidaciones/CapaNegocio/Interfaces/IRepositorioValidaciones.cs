using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaNegocio
{
    // Contrato para ConexionSQL de Validaciones (hereda esto, igual que el ref)
    public interface IRepositorioValidaciones
    {
        void registrarCotizacion(string placa, string ccCliente, int puntosTotal, string resultado);
        CotizacionResultadoDto obtenerCotizacionPorPlaca(string placa);
    }
}
