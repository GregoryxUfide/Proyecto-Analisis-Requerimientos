using hotelproyecto.Models;
using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Data;

namespace hotelproyecto.Data
{
    public class ReporteriaData
    {
        private readonly ConexionDB _conexionDB;

        public ReporteriaData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region "Tabla Productos"
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
                    IdProducto = reader.GetInt32(reader.GetOrdinal("IdProducto")),
                    NombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                    DescripcionProducto = reader.GetString(reader.GetOrdinal("DescripcionProducto")),
                    IdUbicacionProducto = reader.GetInt32(reader.GetOrdinal("IdUbicacionProducto")),
                    CantidadProducto = reader.GetInt32(reader.GetOrdinal("CantidadProducto")),
                    CaducidadProducto = reader.GetDateTime(reader.GetOrdinal("CaducidadProducto")),
                    MarcaProducto = reader.GetString(reader.GetOrdinal("MarcaProducto")),
                    EstadoProducto = reader.GetBoolean(reader.GetOrdinal("EstadoProducto"))
                });
            }

            return lista;
        }
        #endregion

        #region "Tabla Ubicación de Productos"
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
                    IdUbicacionProducto = reader.GetInt32(reader.GetOrdinal("IdUbicacionProducto")),
                    NombreUbicacionProducto = reader.GetString(reader.GetOrdinal("NombreUbicacionProducto")),
                    DescripcionUbicacionProducto = reader.GetString(reader.GetOrdinal("DescripcionUbicacionProducto")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado"))
                });
            }

            return lista;
        }
        #endregion

        #region "Tabla Usuario"

        public async Task<List<Usuario>> ListarUsuariosAsync()
        {
            var lista = new List<Usuario>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUsuarios", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Apellidos = reader.GetString(reader.GetOrdinal("Apellidos")),
                    Gmail = reader.GetString(reader.GetOrdinal("Gmail")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                    RolId = reader.GetInt32(reader.GetOrdinal("RolId"))
                });
            }

            return lista;
        }

        #endregion
        
        #region "Tabla Roles"

        public async Task<List<Rol>> ListarRolesAsync()
        {
            var lista = new List<Rol>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarRoles", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Rol
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                    Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                    Estado = reader.GetBoolean(reader.GetOrdinal("Estado")),
                });
            }

            return lista;
        }

        #endregion
    }
}
