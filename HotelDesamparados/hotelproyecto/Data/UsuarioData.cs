using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Models;

namespace hotelproyecto.Data
{
    public class UsuarioData
    {
        private readonly ConexionDB _conexionDB;

        public UsuarioData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }
        #region"Crear"
        public async Task CrearUsuarioAsync(Usuario usuario)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearUsuario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
            cmd.Parameters.AddWithValue("@Gmail", usuario.Gmail);
            cmd.Parameters.AddWithValue("@Username", usuario.Username);
            cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
            cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
            cmd.Parameters.AddWithValue("@RolId", usuario.RolId);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Actualizar"
        public async Task ActualizarUsuarioAsync(Usuario usuario)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarUsuario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", usuario.Id);
            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
            cmd.Parameters.AddWithValue("@Gmail", usuario.Gmail);
            cmd.Parameters.AddWithValue("@Username", usuario.Username);
            cmd.Parameters.AddWithValue("@Estado", usuario.Estado);
            cmd.Parameters.AddWithValue("@RolId", usuario.RolId);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region "Actualizar Contraseña"
        public async Task ActualizarContrasenaAsync(int id, string contrasena)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarContrasena", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Contrasena", contrasena);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Listar"
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
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellidos = reader.GetString(2),
                    Gmail = reader.GetString(3),
                    Username = reader.GetString(4),
                    Estado = reader.GetBoolean(5),
                    RolId = reader.GetInt32(6),
                    Rol = new Rol { Id = reader.GetInt32(6), Nombre = reader.GetString(7) }
                });
            }
            return lista;
        }
        #endregion

        #region"ObtenerPorId"
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerUsuarioPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellidos = reader.GetString(2),
                    Gmail = reader.GetString(3),
                    Username = reader.GetString(4),
                    Estado = reader.GetBoolean(5),
                    RolId = reader.GetInt32(6),
                    Rol = new Rol { Id = reader.GetInt32(6), Nombre = reader.GetString(7) }
                };
            }
            return null;
        }
        #endregion

        #region"Estado"
        public async Task EstadoUsuarioAsync(int id, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoUsuario", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Estado", estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"FiltroRol"
        public async Task<List<Usuario>> ListarUsuariosPorFiltroAsync(int? rolId, bool? estado, string busqueda = null)
        {
            var lista = new List<Usuario>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUsuariosPorFiltro", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RolId", (object)rolId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Estado", (object)estado ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Busqueda", string.IsNullOrWhiteSpace(busqueda) ? DBNull.Value : busqueda);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellidos = reader.GetString(2),
                    Gmail = reader.GetString(3),
                    Username = reader.GetString(4),
                    Estado = reader.GetBoolean(5),
                    RolId = reader.GetInt32(6),
                    Rol = new Rol { Id = reader.GetInt32(6), Nombre = reader.GetString(7) }
                });
            }
            return lista;
        }
        #endregion

        #region"UsuariosporRol"
        public async Task<List<Usuario>> ObtenerUsuariosPorRolAsync(int rolId)
        {
            var lista = new List<Usuario>();

            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ListarUsuariosPorRol", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@RolId", rolId);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                lista.Add(new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellidos = reader.GetString(2),
                    Gmail = reader.GetString(3)
                });
            }
            return lista;
        }
        #endregion

        #region"Auth"
        public async Task<Usuario?> ObtenerUsuarioPorCorreoAsync(string gmail)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerUsuarioPorCorreo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Gmail", gmail);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Usuario
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Apellidos = reader.GetString(2),
                    Gmail = reader.GetString(3),
                    Username = reader.GetString(4),
                    Contrasena = reader.GetString(5),
                    Estado = reader.GetBoolean(6),
                    RolId = reader.GetInt32(7),
                    Rol = new Rol { Id = reader.GetInt32(7), Nombre = reader.GetString(8) }
                };
            }
            return null;
        }

        public async Task<bool> ExisteCorreoAsync(string gmail)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ExisteCorreo", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Gmail", gmail);

            var resultado = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(resultado) > 0;
        }
        #endregion
    }
}


