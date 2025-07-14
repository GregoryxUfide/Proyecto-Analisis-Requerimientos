using System.Data;
using Sprint_2.Models;
using Microsoft.Data.SqlClient;

namespace Sprint_2.Data
{
    public class HabitacionData
    {
        private readonly ConexionDB _conexionDB;

        public HabitacionData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Crear"
        public async Task CrearHabitacionAsync(Habitacion habitacion)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearHabitacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
            cmd.Parameters.AddWithValue("@Precio", habitacion.Precio);
            cmd.Parameters.AddWithValue("@NumHabitacion", habitacion.NumHabitacion);
            cmd.Parameters.AddWithValue("@NumCamas", habitacion.NumCamas);
            cmd.Parameters.AddWithValue("@Extras", habitacion.Extras ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Comentarios", habitacion.Comentarios ?? (object)DBNull.Value);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarHabitacionAsync(Habitacion habitacion)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarHabitacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", habitacion.Id);
            cmd.Parameters.AddWithValue("@Capacidad", habitacion.Capacidad);
            cmd.Parameters.AddWithValue("@Precio", habitacion.Precio);
            cmd.Parameters.AddWithValue("@NumHabitacion", habitacion.NumHabitacion);
            cmd.Parameters.AddWithValue("@NumCamas", habitacion.NumCamas);
            cmd.Parameters.AddWithValue("@Extras", habitacion.Extras ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Comentarios", habitacion.Comentarios ?? (object)DBNull.Value);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Listar"
        public async Task<List<Habitacion>> ListarHabitacionesAsync()
        {
            var lista = new List<Habitacion>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarHabitaciones", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Habitacion
                {
                    Id = reader.GetInt32(0),
                    Capacidad = reader.GetInt32(1),
                    Precio = reader.GetDecimal(2),
                    NumHabitacion = reader.GetInt32(3),
                    NumCamas = reader.GetInt32(4),
                    Extras = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Comentarios = reader.IsDBNull(6) ? null : reader.GetString(6)
                });
            }
            return lista;
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<Habitacion?> ObtenerHabitacionPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerHabitacionPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Habitacion
                {
                    Id = reader.GetInt32(0),
                    Capacidad = reader.GetInt32(1),
                    Precio = reader.GetDecimal(2),
                    NumHabitacion = reader.GetInt32(3),
                    NumCamas = reader.GetInt32(4),
                    Extras = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Comentarios = reader.IsDBNull(6) ? null : reader.GetString(6)
                };
            }
            return null;
        }
        #endregion

    }
}