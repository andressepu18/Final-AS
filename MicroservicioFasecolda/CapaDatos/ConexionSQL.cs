using Fasecolda.CapaNegocio;
using Fasecolda.CapaNegocio.DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fasecolda.CapaDatos
{
    public class ConexionSQL : IRepositorioAccidentes
    {
        private readonly string _connectionString;

        public ConexionSQL()
        {
            // Compatibilidad: si no se inyecta, leer desde variable de entorno
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? string.Empty;
        }

        // Nuevo constructor para inyección de connection string
        public ConexionSQL(string connectionString)
        {
            _connectionString = connectionString ?? string.Empty;
        }

        public async Task<List<AccidenteDto>> obtenerAccidentesPorPlaca(string placa)
        {
            List<AccidenteDto> accidentes = new List<AccidenteDto>();
            string query = "SELECT idAccidente, placa, fecha, severidad FROM Accidentes WHERE placa = @placa";

            using (SqlConnection conexion = new SqlConnection(_connectionString))
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("@placa", placa);

                    await conexion.OpenAsync();

                    using (SqlDataReader reader = await comando.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            AccidenteDto accidente = new AccidenteDto
                            {
                                IdAccidente = reader.GetInt32(0),
                                Placa = reader.GetString(1),
                                Fecha = reader.GetDateTime(2).ToString("yyyy-MM-dd"),
                                Severidad = reader.GetString(3)
                            };

                            accidentes.Add(accidente);
                        }
                    }
                }
            }

            return accidentes;
        }
    }
}