using System.Data;
using hotelproyecto.Models;
using Microsoft.Data.SqlClient;

namespace hotelproyecto.Data
{
    public class LimpiezaHabitacionData
    {
        private readonly ConexionDB _conexionDB;

        public LimpiezaHabitacionData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Crear"
        public async Task CrearLimpiezaAsync(LimpiezaHabitacion limpieza)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearLimpieza", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TareasCompletadas", limpieza.TareasCompletadas);
            cmd.Parameters.AddWithValue("@NombreConserje", limpieza.NombreConserje);
            cmd.Parameters.Add("@Foto", SqlDbType.VarBinary, -1).Value = limpieza.Foto != null ? limpieza.Foto : (object)DBNull.Value;

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarLimpiezaAsync(LimpiezaHabitacion limpieza)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarLimpieza", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", limpieza.Id);
            cmd.Parameters.AddWithValue("@TareasCompletadas", limpieza.TareasCompletadas);
            cmd.Parameters.AddWithValue("@NombreConserje", limpieza.NombreConserje);
            var fotoParam = cmd.Parameters.Add("@Foto", SqlDbType.VarBinary); fotoParam.Value = limpieza.Foto != null && limpieza.Foto.Length > 0? limpieza.Foto : (object)DBNull.Value;

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Listar"
        public async Task<List<LimpiezaHabitacion>> ListarLimpiezasAsync()
        {
            var lista = new List<LimpiezaHabitacion>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarLimpiezas", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new LimpiezaHabitacion
                {
                    Id = reader.GetInt32(0),
                    TareasCompletadas = reader.GetString(1),
                    NombreConserje = reader.GetString(2),
                    FechaHora = reader.GetDateTime(3)
                });
            }
            return lista;
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<LimpiezaHabitacion?> ObtenerLimpiezaPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerLimpiezaPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new LimpiezaHabitacion
                {
                    Id = reader.GetInt32(0),
                    TareasCompletadas = reader.GetString(1),
                    NombreConserje = reader.GetString(2),
                    Foto = reader.IsDBNull(3) ? null : (byte[])reader["Foto"],
                    FechaHora = reader.GetDateTime(4)
                };
            }
            return null;
        }
        #endregion

        #region "ListarPorConserje"
        public async Task<List<LimpiezaHabitacion>> ListarLimpiezasPorConserjeAsync(string nombreConserje)
        {
            var lista = new List<LimpiezaHabitacion>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarLimpiezasPorConserje", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreConserje", nombreConserje);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new LimpiezaHabitacion
                {
                    Id = reader.GetInt32(0),
                    TareasCompletadas = reader.GetString(1),
                    FechaHora = reader.GetDateTime(2)
                });
            }
            return lista;
        }
        #endregion
    }
}