using Microsoft.Data.SqlClient;
using Validaciones.CapaNegocio;
using Validaciones.CapaNegocio.DTOs;

namespace Validaciones.CapaDatos
{
    public class ConexionSQL : IRepositorioValidaciones
    {
        public SqlConnection conexion;

        public ConexionSQL()
        {
            this.conexion = new SqlConnection("");
        }

        public CotizacionResultadoDto obtenerCotizacionPorPlaca(string placa)
        {
            string query = "SELECT TOP 1 placa, ccCliente, puntosTotal, resultado FROM Cotizaciones WHERE placa = @placa ORDER BY idCotizacion DESC";

            using (SqlCommand cmd = new SqlCommand(query, conexion))
            {
                cmd.Parameters.AddWithValue("@placa", placa);

                conexion.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new CotizacionResultadoDto
                        {
                            Cotizacion = reader.GetInt32(2) < 400,
                            Placa = reader.GetString(0),
                            CcCliente = reader.GetString(1),
                            Puntos = reader.GetInt32(2),
                            Resultado = reader.GetString(3)
                        };
                    }
                }

                conexion.Close();
            }

            return new CotizacionResultadoDto();
        }

        public ConexionSQL(string connectionString)
        {
            this.conexion = new SqlConnection(connectionString ?? string.Empty);
        }

        public void registrarCotizacion(string placa, string ccCliente, int puntosTotal, string resultado)
        {
            string insert = string.Format(
                "INSERT INTO Cotizaciones (placa, ccCliente, puntosTotal, resultado) VALUES ('{0}', '{1}', {2}, '{3}')",
                placa, ccCliente, puntosTotal, resultado
            );

            conexion.Open();

            SqlCommand comando = new SqlCommand(insert, conexion);
            comando.ExecuteNonQuery();

            conexion.Close();
        }
    }
}
