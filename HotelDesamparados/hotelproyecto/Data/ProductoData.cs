using Microsoft.Data.SqlClient;
using hotelproyecto.Models;
using System.Data;

namespace hotelproyecto.Data
{
    public class ProductoData
    {
        private readonly ConexionDB _conexionDB;

        public ProductoData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }
        #region"Listar"
        public async Task<List<Producto>> ListarProductosAsync()
        {
            var lista = new List<Producto>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarProductos", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    NombreProducto = reader.GetString(1),
                    DescripcionProducto = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IdUbicacionProducto = reader.GetInt32(3),
                    UbicacionProducto = new UbicacionProducto
                    {
                        NombreUbicacionProducto = reader.GetString(4)
                    },
                    CantidadProducto = reader.GetInt32(5),
                    CaducidadProducto = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                    MarcaProducto = reader.IsDBNull(7) ? null : reader.GetString(7),
                    EstadoProducto = reader.GetBoolean(8)
                });
            }
            return lista;
        }
        #endregion

        #region"Crear"
        public async Task CrearProductoAsync(Producto producto)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearProducto", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
            cmd.Parameters.AddWithValue("@DescripcionProducto", (object)producto.DescripcionProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IdUbicacionProducto", producto.IdUbicacionProducto);
            cmd.Parameters.AddWithValue("@CantidadProducto", producto.CantidadProducto);
            cmd.Parameters.AddWithValue("@CaducidadProducto", (object)producto.CaducidadProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MarcaProducto", (object)producto.MarcaProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EstadoProducto", producto.EstadoProducto);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"ObtenerPorId"
        public async Task<Producto?> ObtenerProductoPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerProductoPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdProducto", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    NombreProducto = reader.GetString(1),
                    DescripcionProducto = reader.IsDBNull(2) ? null : reader.GetString(2),
                    IdUbicacionProducto = reader.GetInt32(3),
                    UbicacionProducto = new UbicacionProducto
                    {
                        NombreUbicacionProducto = reader.GetString(4)
                    },
                    CantidadProducto = reader.GetInt32(5),
                    CaducidadProducto = reader.IsDBNull(6) ? null : reader.GetDateTime(6),
                    MarcaProducto = reader.IsDBNull(7) ? null : reader.GetString(7),
                    EstadoProducto = reader.GetBoolean(8)
                };
            }
            return null;
        }
        #endregion

        #region"Actualizar"
        public async Task EditarProductoAsync(Producto producto)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarProducto", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
            cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
            cmd.Parameters.AddWithValue("@DescripcionProducto", (object)producto.DescripcionProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@IdUbicacionProducto", producto.IdUbicacionProducto);
            cmd.Parameters.AddWithValue("@CantidadProducto", producto.CantidadProducto);
            cmd.Parameters.AddWithValue("@CaducidadProducto", (object)producto.CaducidadProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MarcaProducto", (object)producto.MarcaProducto ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EstadoProducto", producto.EstadoProducto);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Estado"
        public async Task EstadoProductoAsync(int id, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoProducto", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdProducto", id);
            cmd.Parameters.AddWithValue("@EstadoProducto", estado);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
#endregion
