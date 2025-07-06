using Microsoft.Data.SqlClient;
using Sprint_2.Models;
using System.Data;

namespace Sprint_2.Data
{
    public class RolData
    {
        private readonly ConexionDB _conexionDB;

        public RolData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }
        #region"Crear"
        public async Task CrearRolAsync(Rol rol)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_CrearRol", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);
            cmd.Parameters.AddWithValue("@Estado", rol.Estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Actualizar"
        public async Task ActualizarRolAsync(Rol rol)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ActualizarRol", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", rol.Id);
            cmd.Parameters.AddWithValue("@Nombre", rol.Nombre);
            cmd.Parameters.AddWithValue("@Descripcion", rol.Descripcion);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"Listar"
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
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2),
                    Estado = reader.GetBoolean(3)
                });
            }
            return lista;
        }
        #endregion

        #region"ObtenerPorId"
        public async Task<Rol?> ObtenerRolPorIdAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerRolPorId", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Rol
                {
                    Id = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2),
                    Estado = reader.GetBoolean(3)
                };
            }
            return null;
        }
        #endregion

        #region"Estado"
        public async Task CambiarEstadoRolAsync(int id, bool estado)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_EstadoRol", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Estado", estado);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region"RolPorNombre"
        public async Task<int?> ObtenerRolPorNombreAsync(string nombre)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerRolPorNombre", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Nombre", nombre);

            var resultado = await cmd.ExecuteScalarAsync();
            if (resultado != null && int.TryParse(resultado.ToString(), out int id))
            {
                return id;
            }
            return null;
        }
    }
}
#endregion
