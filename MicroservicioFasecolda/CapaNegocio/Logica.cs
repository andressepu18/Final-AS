using System.Collections.Generic;
using System.Threading.Tasks;
using Fasecolda.CapaNegocio.DTOs;

namespace Fasecolda.CapaNegocio
{
    public class Logica
    {
        private readonly IRepositorioAccidentes fuenteDatos;

        public Logica(IRepositorioAccidentes fuenteDatos)
        {
            this.fuenteDatos = fuenteDatos;
        }

        public async Task<List<AccidenteDto>> consultarAccidentesPorPlaca(string placa)
        {
            return await this.fuenteDatos.obtenerAccidentesPorPlaca(placa);
        }
    }
}