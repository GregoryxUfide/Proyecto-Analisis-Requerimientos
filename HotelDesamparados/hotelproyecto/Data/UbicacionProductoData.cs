using Microsoft.Data.SqlClient;
using hotelproyecto.Models;
using System.Data;

namespace hotelproyecto.Data
{
    public class UbicacionProductoData
    {
        private readonly ConexionDB _conexionDB;

        public UbicacionProductoData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region"Crear"
        public async Task CrearUbicacionAsync(UbicacionProducto ubicacion)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearUbicacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreUbicacionProducto", ubicacion.NombreUbicacionProducto);
            cmd.Parameters.AddWithValue("@DescripcionUbicacionProducto", ubicacion.DescripcionUbicacionProducto);
            cmd.Parameters.AddWithValue("@Estado", ubicacion.Estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Actualizar"
        public async Task ActualizarUbicacionAsync(UbicacionProducto ubicacion)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarUbicacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUbicacionProducto", ubicacion.IdUbicacionProducto);
            cmd.Parameters.AddWithValue("@NombreUbicacionProducto", ubicacion.NombreUbicacionProducto);
            cmd.Parameters.AddWithValue("@DescripcionUbicacionProducto", ubicacion.DescripcionUbicacionProducto);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Listar"     
        public async Task<List<UbicacionProducto>> ListarUbicacionesAsync()
        {
            var lista = new List<UbicacionProducto>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUbicaciones", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new UbicacionProducto
                {
                    IdUbicacionProducto = reader.GetInt32(0),
                    NombreUbicacionProducto = reader.GetString(1),
                    DescripcionUbicacionProducto = reader.GetString(2),
                    Estado = reader.GetBoolean(3)
                });
            }
            return lista;
        }
        #endregion

        #region"ObtenerPorId"
        public async Task<UbicacionProducto?> ObtenerUbicacionPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerUbicacionPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUbicacionProducto", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new UbicacionProducto
                {
                    IdUbicacionProducto = reader.GetInt32(0),
                    NombreUbicacionProducto = reader.GetString(1),
                    DescripcionUbicacionProducto = reader.GetString(2),
                    Estado = reader.GetBoolean(3)
                };
            }
            return null;
        }
        #endregion

        #region"Estado"
        public async Task CambiarEstadoUbicacionAsync(int id, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoUbicacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdUbicacionProducto", id);
            cmd.Parameters.AddWithValue("@Estado", estado);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
#endregion
