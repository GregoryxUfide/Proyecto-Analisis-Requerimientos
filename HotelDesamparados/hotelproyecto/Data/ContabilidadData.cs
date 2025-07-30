using Microsoft.Data.SqlClient;
using hotelproyecto.Data;
using hotelproyecto.Models;
using System.Data;

namespace hotelproyecto.Data
{
    public class ContabilidadData
    {
        private readonly ConexionDB _conexionDB;

        public ContabilidadData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Listar"
        public async Task<List<Contabilidad>> ListarContabilidadAsync()
        {
            var lista = new List<Contabilidad>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarContabilidad", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Contabilidad
                {
                    IdContabilidad = reader.GetInt32(0),
                    Fecha = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                    Monto = reader.GetDecimal(2),
                    Detalle = reader.GetString(3),
                    Comentario = reader.GetString(4)
                });
            }
            return lista;
        }
        #endregion

        #region "Crear"
        public async Task CrearContabilidadAsync(Contabilidad contabilidad)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearContabilidad", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Fecha", (object?)contabilidad.Fecha ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Monto", contabilidad.Monto);
            cmd.Parameters.AddWithValue("@Detalle", contabilidad.Detalle);
            cmd.Parameters.AddWithValue("@Comentario", contabilidad.Comentario);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "ObtenerPorId"
        public async Task<Contabilidad?> ObtenerContabilidadPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerContabilidadPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdContabilidad", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Contabilidad
                {
                    IdContabilidad = reader.GetInt32(0),
                    Fecha = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                    Monto = reader.GetDecimal(2),
                    Detalle = reader.GetString(3),
                    Comentario = reader.GetString(4)
                };
            }
            return null;
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarContabilidadAsync(Contabilidad contabilidad)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarContabilidad", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdContabilidad", contabilidad.IdContabilidad);
            cmd.Parameters.AddWithValue("@Fecha", (object?)contabilidad.Fecha ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Monto", contabilidad.Monto);
            cmd.Parameters.AddWithValue("@Detalle", contabilidad.Detalle);
            cmd.Parameters.AddWithValue("@Comentario", contabilidad.Comentario);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Filtro"
        public async Task<List<Contabilidad>> FiltrarPorFechaAsync(int? mes, int? anio)
        {
            var lista = new List<Contabilidad>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_FiltrarContabilidadPorFecha", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Mes", (object?)mes ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Anio", (object?)anio ?? DBNull.Value);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Contabilidad
                {
                    IdContabilidad = reader.GetInt32(0),
                    Fecha = reader.IsDBNull(1) ? null : reader.GetDateTime(1),
                    Monto = reader.GetDecimal(2),
                    Detalle = reader.GetString(3),
                    Comentario = reader.GetString(4)
                });
            }

            return lista;
        }


        #endregion

    }
}
