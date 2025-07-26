using hotelproyecto.Data;
using hotelproyecto.Models;
using BCrypt.Net;

namespace hotelproyecto.Services
{
    public class AuthService
    {
        private readonly UsuarioData _usuarioData;
        private readonly RolData _rolData;
        private readonly TokenRecuperacionService _tokenRecuperacionService;


        public AuthService(UsuarioData usuarioData, RolData rolData, TokenRecuperacionService tokenRecuperacionService)
        {
            _usuarioData = usuarioData;
            _rolData = rolData;
            _tokenRecuperacionService = tokenRecuperacionService;
        }

        #region "Validar Login"
        public async Task<Usuario?> ValidarCredencialesAsync(string gmail, string contrasena)
        {
            var usuario = await _usuarioData.ObtenerUsuarioPorCorreoAsync(gmail);
            if (usuario == null || !usuario.Estado) return null;

            bool valido = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena);
            return valido ? usuario : null;
        }
        #endregion

        #region "Registro"
        public async Task<(bool Exito, string Mensaje)> RegistrarUsuarioAsync(Usuario usuario)
        {
            bool existeCorreo = await _usuarioData.ExisteCorreoAsync(usuario.Gmail);
            if (existeCorreo)
                return (false, "Este correo ya está registrado.");

            var rolId = await _rolData.ObtenerRolPorNombreAsync("Usuario");
            if (rolId == null)
                return (false, "El rol por defecto no existe.");

            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);
            usuario.Estado = true;
            usuario.RolId = rolId.Value;

            await _usuarioData.CrearUsuarioAsync(usuario);
            return (true, "Usuario creado exitosamente.");
        }
        #endregion

        #region "Actualizar Contraseña"
        public async Task ActualizarContrasenaAsync(int id, string nuevaContrasena)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            await _usuarioData.ActualizarContrasenaAsync(id, hash);
        }
        #endregion

        #region "Recuperacion"
        public async Task<string> GenerarTokenRecuperacionAsync(int usuarioId)
        {
            return await _tokenRecuperacionService.GenerarYGuardarTokenAsync(usuarioId);
        }

        public async Task<bool> ValidarTokenRecuperacionAsync(int usuarioId, string token)
        {
            return await _tokenRecuperacionService.ValidarTokenAsync(usuarioId, token);
        }

        public async Task MarcarTokenComoUsadoAsync(int usuarioId, string token)
        {
            await _tokenRecuperacionService.MarcarTokenComoUsadoAsync(usuarioId, token);
        }
        #endregion
    }
}
