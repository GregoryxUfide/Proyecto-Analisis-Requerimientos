using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Models;

namespace hotelproyecto.Data
{
    public class ReservaData
    {
        private readonly ConexionDB _conexionDB;

        public ReservaData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Crear"
        public async Task<(int Codigo, string Mensaje)> CrearReservaAsync(Reserva reserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fechaInicio", reserva.FechaInicio);
            cmd.Parameters.AddWithValue("@fechaFinal", reserva.FechaFinal);
            cmd.Parameters.AddWithValue("@nombreReservante", reserva.NombreReservante);
            cmd.Parameters.AddWithValue("@telefono", reserva.Telefono);
            cmd.Parameters.AddWithValue("@correo", reserva.Correo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@numHabitacion", reserva.NumHabitacion);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                int codigo = reader.GetInt32(0);
                string mensaje = reader.GetString(1);
                return (codigo, mensaje);
            }

            return (-99, "Error desconocido al crear la reserva.");
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarReservaAsync(Reserva reserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
            cmd.Parameters.AddWithValue("@fechaInicio", reserva.FechaInicio);
            cmd.Parameters.AddWithValue("@fechaFinal", reserva.FechaFinal);
            cmd.Parameters.AddWithValue("@nombreReservante", reserva.NombreReservante);
            cmd.Parameters.AddWithValue("@telefono", reserva.Telefono);
            cmd.Parameters.AddWithValue("@correo", reserva.Correo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@numHabitacion", reserva.NumHabitacion);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Listar"
        public async Task<List<Reserva>> ListarReservasAsync()
        {
            var lista = new List<Reserva>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarReservas", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Reserva
                {
                    IdReserva = reader.GetInt32(0),
                    FechaInicio = reader.GetDateTime(1),
                    FechaFinal = reader.GetDateTime(2),
                    NombreReservante = reader.GetString(3),
                    Telefono = reader.GetInt32(4),
                    Correo = reader.IsDBNull(5) ? null : reader.GetString(5),
                    NumHabitacion = reader.GetInt32(6)
                });
            }
            return lista;
        }
        #endregion

        #region "ListarPorFechaInicio"
        public async Task<List<Reserva>> ListarReservasPorFechaInicioAsync(DateTime fechaDesde)
        {
            var lista = new List<Reserva>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarReservaPorFecha", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fechaDesde", fechaDesde);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Reserva
                {
                    IdReserva = reader.GetInt32(0),
                    FechaInicio = reader.GetDateTime(1),
                    FechaFinal = reader.GetDateTime(2),
                    NombreReservante = reader.GetString(3),
                    Telefono = reader.GetInt32(4),
                    Correo = reader.IsDBNull(5) ? null : reader.GetString(5),
                    NumHabitacion = reader.GetInt32(6)
                });
            }
            return lista;
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<Reserva?> ObtenerReservaPorIdAsync(int idReserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerReservaPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdReserva", idReserva);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Reserva
                {
                    IdReserva = reader.GetInt32(0),
                    FechaInicio = reader.GetDateTime(1),
                    FechaFinal = reader.GetDateTime(2),
                    NombreReservante = reader.GetString(3),
                    Telefono = reader.GetInt32(4),
                    Correo = reader.IsDBNull(5) ? null : reader.GetString(5),
                    NumHabitacion = reader.GetInt32(6)
                };
            }
            return null;
        }
        #endregion

        #region "Eliminar"
        public async Task EliminarReservaAsync(int idReserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EliminarReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdReserva", idReserva);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
