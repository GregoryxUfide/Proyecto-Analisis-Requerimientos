using System.Data;
using Microsoft.Data.SqlClient;
using hotelproyecto.Models;

namespace hotelproyecto.Data
{
    public class TokenRecuperacionData
    {
        private readonly ConexionDB _conexionDB;

        public TokenRecuperacionData(ConexionDB conexionDB)
        {
            _conexionDB = conexionDB;
        }

        #region Guardar Token
        public async Task GuardarTokenAsync(int usuarioId, string token, DateTime expiracion)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_GuardarTokenRecuperacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
            cmd.Parameters.AddWithValue("@Token", token);
            cmd.Parameters.AddWithValue("@FechaExpiracion", expiracion);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion

        #region Obtener Token por Usuario
        public async Task<TokenRecuperacion?> ObtenerTokenPorUsuarioAsync(int usuarioId)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_ObtenerTokenRecuperacion", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new TokenRecuperacion
                {
                    Id = reader.GetInt32(0),
                    UsuarioId = reader.GetInt32(1),
                    Token = reader.GetString(2),
                    FechaCreacion = reader.GetDateTime(3),
                    FechaExpiracion = reader.GetDateTime(4),
                    Usado = reader.GetBoolean(5)
                };
            }
            return null;
        }
        #endregion

        #region Marcar Token como Usado
        public async Task MarcarTokenComoUsadoAsync(int id)
        {
            using var conexion = await _conexionDB.ObtenerConexionAsync();
            using var cmd = new SqlCommand("sp_MarcarTokenComoUsado", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", id);

            await cmd.ExecuteNonQueryAsync();
        }
        #endregion
    }
}
