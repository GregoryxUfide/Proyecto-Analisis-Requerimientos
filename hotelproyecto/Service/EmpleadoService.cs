using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class EmpleadoService
    {
        private readonly EmpleadoData _empleadoData;
        private readonly RolData _rolData;
        private readonly UsuarioData _usuarioData;

        public EmpleadoService(EmpleadoData empleadoData, RolData rolData, UsuarioData usuarioData)
        {
            _empleadoData = empleadoData;
            _rolData = rolData;
            _usuarioData = usuarioData;
        }
        #region Listar
        public async Task<List<EmpleadoViewModel>> ListarEmpleadosViewModelAsync()
        {
            var empleados = await _empleadoData.ListarEmpleadosAsync();

            return empleados.Select(e => new EmpleadoViewModel
            {
                Id = e.Id,
                NumeroEmpleado = e.NumeroEmpleado,
                SalarioEmpleado = e.SalarioEmpleado,
                UsuarioId = e.UsuarioId,
                UsuarioUsername = e.Usuario?.Username, // Username visible en Index
                RolId = e.RolId,
                RolNombre = e.Rol?.Nombre,             // Nombre de rol visible en Index
                Estado = e.Estado
                // No se cargan UsuariosDisponibles ni RolesDisponibles aquí
            }).ToList();
        }
        #endregion

        #region Crear
        public async Task CrearEmpleadoAsync(EmpleadoViewModel vm)
        {
            var empleado = new Empleado
            {
                NumeroEmpleado = vm.NumeroEmpleado,
                SalarioEmpleado = vm.SalarioEmpleado,
                UsuarioId = vm.UsuarioId,
                RolId = vm.RolId,
                Estado = true
            };

            await _empleadoData.CrearEmpleadoAsync(empleado);
        }
        #endregion

        #region ObtenerporId 
        public async Task<EmpleadoViewModel?> ObtenerEmpleadoViewModelPorIdAsync(int id)
        {
            var empleado = await _empleadoData.ObtenerEmpleadoPorIdAsync(id);
            if (empleado == null) return null;

            var roles = await _rolData.ListarRolesAsync();
            var usuarios = await _usuarioData.ListarUsuariosPorFiltroAsync(empleado.RolId, null, null);

            return new EmpleadoViewModel
            {
                Id = empleado.Id,
                NumeroEmpleado = empleado.NumeroEmpleado,
                SalarioEmpleado = empleado.SalarioEmpleado,
                UsuarioId = empleado.UsuarioId,
                RolId = empleado.RolId,
                Estado = empleado.Estado,
                RolesDisponibles = roles.Select(r => new RolViewModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    Estado = r.Estado
                }).ToList(),
                UsuariosDisponibles = usuarios.Select(u => new UsuarioViewModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellidos = u.Apellidos,
                    Gmail = u.Gmail,
                    Username = u.Username,
                    Estado = u.Estado,
                    RolId = u.RolId
                }).ToList()
            };
        }
        #endregion

        #region Actualizar
        public async Task ActualizarEmpleadoAsync(EmpleadoViewModel vm)
        {
            var empleado = new Empleado
            {
                Id = vm.Id,
                NumeroEmpleado = vm.NumeroEmpleado,
                SalarioEmpleado = vm.SalarioEmpleado,
                RolId = vm.RolId,
                Estado = vm.Estado
            };

            await _empleadoData.ActualizarEmpleadoAsync(empleado);
        }
        #endregion

        #region Estado
        public async Task CambiarEstadoEmpleadoAsync(int id, bool estado)
        {
            await _empleadoData.CambiarEstadoEmpleadoAsync(id, estado);
        }
        #endregion
    }
}
