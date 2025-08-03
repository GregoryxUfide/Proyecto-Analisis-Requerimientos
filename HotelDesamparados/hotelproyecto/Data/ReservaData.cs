using Microsoft.Data.SqlClient;
using hotelproyecto.Models;
using System.Data;

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
        public async Task CrearReservaAsync(Reserva reserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFinal", reserva.FechaFinal);
            cmd.Parameters.AddWithValue("@Nombre", reserva.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", reserva.Telefono);
            cmd.Parameters.AddWithValue("@Correo", reserva.Correo);
            cmd.Parameters.AddWithValue("@HabitacionId", reserva.HabitacionId);            

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarReservaAsync(Reserva reserva)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdReserva", reserva.IdReserva);
            cmd.Parameters.AddWithValue("@FechaInicio", reserva.FechaInicio);
            cmd.Parameters.AddWithValue("@FechaFinal", reserva.FechaFinal);
            cmd.Parameters.AddWithValue("@Nombre", reserva.Nombre);
            cmd.Parameters.AddWithValue("@Telefono", reserva.Telefono);
            cmd.Parameters.AddWithValue("@Correo", reserva.Correo);
            cmd.Parameters.AddWithValue("@HabitacionId", reserva.HabitacionId);
            cmd.Parameters.AddWithValue("@Estado" , reserva.Estado);

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
                    Nombre = reader.GetString(3),
                    Telefono = reader.GetString(4),
                    Correo = reader.GetString(5),
                    HabitacionId = reader.GetInt32(6),
                    Estado = reader.GetBoolean(7)
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
                    Nombre = reader.GetString(3),
                    Telefono = reader.GetString(4),
                    Correo = reader.GetString(5),
                    HabitacionId = reader.GetInt32(6),
                    Estado = reader.GetBoolean(7)
                };
            }
            return null;
        }
        #endregion

        #region "Estado"
        public async Task CambiarEstadoReservaAsync(int idReserva, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoReserva", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdReserva", idReserva);
            cmd.Parameters.AddWithValue("@Estado", estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
