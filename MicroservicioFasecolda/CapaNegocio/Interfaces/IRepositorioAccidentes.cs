using System.Collections.Generic;
using System.Threading.Tasks;
using Fasecolda.CapaNegocio.DTOs;

namespace Fasecolda.CapaNegocio
{
    public interface IRepositorioAccidentes
    {
        Task<List<AccidenteDto>> obtenerAccidentesPorPlaca(string placa);
    }
}