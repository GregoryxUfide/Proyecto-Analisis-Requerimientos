using Microsoft.Data.SqlClient;
using hotelproyecto.Models;
using System.Data;

namespace hotelproyecto.Data
{
    public class PuntoVentaData
    {
        private readonly ConexionDB _conexionDB;

        public PuntoVentaData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Crear"
        public async Task CrearPuntoVentaAsync(PuntoVenta puntoVenta)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearPuntoVenta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DescripcionVenta", puntoVenta.DescripcionVenta);
            cmd.Parameters.AddWithValue("@Metodo_Pago", puntoVenta.Metodo_Pago);
            cmd.Parameters.AddWithValue("@Descuento", puntoVenta.Descuento);
            cmd.Parameters.AddWithValue("@ReservaId", puntoVenta.ReservaId);
            cmd.Parameters.AddWithValue("@EmpleadoId", puntoVenta.EmpleadoId);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarPuntoVentaAsync(PuntoVenta puntoVenta)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarPuntoVenta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", puntoVenta.Id);
            cmd.Parameters.AddWithValue("@DescripcionVenta", puntoVenta.DescripcionVenta);
            cmd.Parameters.AddWithValue("@Metodo_Pago", puntoVenta.Metodo_Pago);
            cmd.Parameters.AddWithValue("@Descuento", puntoVenta.Descuento);
            cmd.Parameters.AddWithValue("@ReservaId", puntoVenta.ReservaId);
            cmd.Parameters.AddWithValue("@EmpleadoId", puntoVenta.EmpleadoId);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Listar"
        public async Task<List<PuntoVenta>> ListarPuntosVentaAsync()
        {
            var lista = new List<PuntoVenta>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarPuntoVenta", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new PuntoVenta
                {
                    Id = reader.GetInt32(0),
                    DescripcionVenta = reader.GetString(1),
                    Metodo_Pago = reader.GetString(2),
                    Descuento = reader.GetDecimal(3),
                    ReservaId = reader.GetInt32(4),       // IdReserva
                    EmpleadoId = reader.GetInt32(6)       // UsuarioId del empleado
                    // Nota: Si quieres traer el correo reserva y nombre empleado puedes mapear en un ViewModel o DTO separado
                });
            }
            return lista;
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<PuntoVenta?> ObtenerPuntoVentaPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerPuntoVentaPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new PuntoVenta
                {
                    Id = reader.GetInt32(0),
                    DescripcionVenta = reader.GetString(1),
                    Metodo_Pago = reader.GetString(2),
                    Descuento = reader.GetDecimal(3),
                    ReservaId = reader.GetInt32(4),       // IdReserva
                    EmpleadoId = reader.GetInt32(6)       // UsuarioId del empleado
                };
            }
            return null;
        }
        #endregion
    }
}
