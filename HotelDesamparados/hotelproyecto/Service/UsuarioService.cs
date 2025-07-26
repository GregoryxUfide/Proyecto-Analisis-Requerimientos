using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class UsuarioService
    {
        private readonly UsuarioData _usuarioData;
        private readonly RolService _rolService;

        public UsuarioService(UsuarioData usuarioData, RolService rolService)
        {
            _usuarioData = usuarioData;
            _rolService = rolService;
        }

        #region "Crear"
        public async Task CrearUsuarioAsync(UsuarioViewModel vm)
        {
            var usuario = new Usuario
            {
                Nombre = vm.Nombre,
                Apellidos = vm.Apellidos,
                Gmail = vm.Gmail,
                Username = vm.Username,
                Contrasena = BCrypt.Net.BCrypt.HashPassword(vm.Contrasena),
                Estado = vm.Estado,
                RolId = vm.RolId
            };

            await _usuarioData.CrearUsuarioAsync(usuario);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarUsuarioAsync(UsuarioViewModel vm)
        {
            var usuario = new Usuario
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellidos = vm.Apellidos,
                Gmail = vm.Gmail,
                Username = vm.Username,
                Estado = vm.Estado,
                RolId = vm.RolId
            };

            await _usuarioData.ActualizarUsuarioAsync(usuario);
        }

        #endregion

        #region "Actualizar Contraseña"
        public async Task ActualizarContrasenaAsync(int id, string nuevaContrasena)
        {
            string hash = BCrypt.Net.BCrypt.HashPassword(nuevaContrasena);
            await _usuarioData.ActualizarContrasenaAsync(id, hash);
        }
        #endregion

        #region "Listar"
        public async Task<List<UsuarioViewModel>> ListarUsuariosViewModelAsync()
        {
            var usuarios = await _usuarioData.ListarUsuariosAsync();

            return usuarios.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellidos = u.Apellidos,
                Gmail = u.Gmail,
                Username = u.Username,
                Estado = u.Estado,
                RolId = u.RolId,                
            }).ToList();
        }
        #endregion

        #region "ObtenerporId"
        public async Task<UsuarioViewModel?> ObtenerUsuarioViewModelPorIdAsync(int id)
        {
            var usuario = await _usuarioData.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null) return null;

            var roles = await _rolService.ListarRolesAsync();

            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Gmail = usuario.Gmail,
                Username = usuario.Username,                
                Estado = usuario.Estado,
                RolId = usuario.RolId,
                Roles = roles
            };
        }
        #endregion

        #region "Estado"
        public async Task CambiarEstadoUsuarioAsync(int id, bool estado)
        {
            await _usuarioData.EstadoUsuarioAsync(id, estado);
        }
        #endregion

        #region"FiltroRol"
        public async Task<List<UsuarioViewModel>> ListarUsuariosViewModelAsync(int? rolId, bool? estado, string busqueda = null)
        {
            var usuarios = await _usuarioData.ListarUsuariosPorFiltroAsync(rolId, estado, busqueda);

            var usuariosVm = usuarios.Select(u => new UsuarioViewModel
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Apellidos = u.Apellidos,
                Gmail = u.Gmail,
                Username = u.Username,
                Estado = u.Estado,
                RolId = u.RolId,
                Roles = new List<RolViewModel> { _rolService.MapearARolViewModel(u.Rol) }

            }).ToList();

            return usuariosVm;
        }
        #endregion

        #region "Auth"
        public async Task<Usuario?> ValidarCredencialesAsync(string gmail, string contrasena)
        {
            var usuario = await _usuarioData.ObtenerUsuarioPorCorreoAsync(gmail);
            if (usuario == null || !usuario.Estado) return null;

            bool valido = BCrypt.Net.BCrypt.Verify(contrasena, usuario.Contrasena);
            return valido ? usuario : null;
        }

        public async Task<bool> ExisteCorreoAsync(string gmail)
        {
            return await _usuarioData.ExisteCorreoAsync(gmail);
        }
        #endregion
        
        }
    }
