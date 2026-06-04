using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaNegocio
{
    public class Logica
    {
        private IClienteFasecolda        fuenteDatosExternos;
        private IRepositorioValidaciones fuenteDatosPropios;

        public Logica(IClienteFasecolda fuenteDatosExternos, IRepositorioValidaciones fuenteDatosPropios)
        {
            this.fuenteDatosExternos = fuenteDatosExternos;
            this.fuenteDatosPropios  = fuenteDatosPropios;
        }

        public bool procesarSolicitud(SolicitudDto solicitud)
        {
            // Paso 1: obtener lista cruda de accidentes desde Fasecolda
            List<AccidenteFasecoldaDto> accidentes =
                this.fuenteDatosExternos.obtenerAccidentesPorPlaca(solicitud.Placa);

            // Paso 2: recorrer con foreach y sumar puntos según severidad
            int puntosTotal = 0;
            foreach (AccidenteFasecoldaDto accidente in accidentes)
            {
                string severidad = accidente.Severidad.Trim().ToLower();

                if (severidad == "solo latas")
                    puntosTotal += 100;
                else if (severidad == "heridos")
                    puntosTotal += 200;
                else if (severidad == "muertos")
                    puntosTotal += 300;
            }

            // Paso 3: regla de aprobación (>= 400 → rechazada)
            bool   aprobada  = puntosTotal < 400;
            string resultado = aprobada ? "aprobada" : "rechazada";

            // Paso 4: persistir en Db_Validaciones
            // "cedulaCliente" (DTO entrada) → columna "ccCliente" (BD)
            this.fuenteDatosPropios.registrarCotizacion(
                solicitud.Placa,
                solicitud.CedulaCliente,
                puntosTotal,
                resultado
            );

            return aprobada;
        }

        public CotizacionResultadoDto procesarSolicitudConResultado(SolicitudDto solicitud)
        {
            List<AccidenteFasecoldaDto> accidentes =
                this.fuenteDatosExternos.obtenerAccidentesPorPlaca(solicitud.Placa);

            int puntosTotal = 0;
            foreach (AccidenteFasecoldaDto accidente in accidentes)
            {
                string severidad = accidente.Severidad.Trim().ToLower();

                if (severidad == "solo latas")
                    puntosTotal += 100;
                else if (severidad == "heridos")
                    puntosTotal += 200;
                else if (severidad == "muertos")
                    puntosTotal += 300;
            }

            bool aprobada = puntosTotal < 400;
            string resultado = aprobada ? "aprobada" : "rechazada";

            this.fuenteDatosPropios.registrarCotizacion(
                solicitud.Placa,
                solicitud.CedulaCliente,
                puntosTotal,
                resultado
            );

            return new CotizacionResultadoDto
            {
                Cotizacion = aprobada,
                Placa = solicitud.Placa,
                CcCliente = solicitud.CedulaCliente,
                Puntos = puntosTotal,
                Resultado = resultado
            };
        }

        // Método auxiliar: calcular puntos sin persistir
        public int calcularPuntosPorPlaca(string placa)
        {
            List<AccidenteFasecoldaDto> accidentes =
                this.fuenteDatosExternos.obtenerAccidentesPorPlaca(placa);

            int puntosTotal = 0;
            foreach (AccidenteFasecoldaDto accidente in accidentes)
            {
                string severidad = accidente.Severidad.Trim().ToLower();

                if (severidad == "solo latas")
                    puntosTotal += 100;
                else if (severidad == "heridos")
                    puntosTotal += 200;
                else if (severidad == "muertos")
                    puntosTotal += 300;
            }

            return puntosTotal;
        }

        // Obtener última cotización persistida por placa
        public CotizacionResultadoDto obtenerCotizacionPorPlaca(string placa)
        {
            return this.fuenteDatosPropios.obtenerCotizacionPorPlaca(placa);
        }
    }
}
