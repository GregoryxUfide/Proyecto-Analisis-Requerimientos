using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.Services
{
    public class EmpleadoService
    {        
        private readonly EmpleadoData _empleadoData;
        private readonly UsuarioData _usuarioData;
        private readonly UsuarioService _usuarioService;

        public EmpleadoService(EmpleadoData empleadoData, UsuarioData usuarioData, UsuarioService usuarioService)
        {
            _empleadoData = empleadoData;
            _usuarioData = usuarioData;
            _usuarioService = usuarioService;
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
                UsuarioNombre = e.Usuario?.Username,
                RolNombre = e.Usuario?.Rol?.Nombre,
                Estado = e.Estado
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
                Estado = true
            };

            await _empleadoData.CrearEmpleadoAsync(empleado);
        }
        #endregion

        #region Obtener por ID
        public async Task<EmpleadoViewModel?> ObtenerEmpleadoViewModelPorIdAsync(int id)
        {
            var empleado = await _empleadoData.ObtenerEmpleadoPorIdAsync(id);
            if (empleado == null) return null;

            var usuarios = await _usuarioService.ListarUsuariosViewModelAsync(null, true);

            return new EmpleadoViewModel
            {
                Id = empleado.Id,
                NumeroEmpleado = empleado.NumeroEmpleado,
                SalarioEmpleado = empleado.SalarioEmpleado,
                UsuarioId = empleado.UsuarioId,
                UsuarioNombre = empleado.Usuario?.Username,
                RolNombre = empleado.Usuario?.Rol?.Nombre,
                Estado = empleado.Estado,
                UsuariosLista = usuarios
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
                UsuarioId = vm.UsuarioId,
                Estado = vm.Estado
            };

            await _empleadoData.ActualizarEmpleadoAsync(empleado);
        }
        #endregion

        #region Cambiar Estado
        public async Task CambiarEstadoEmpleadoAsync(int id, bool estado)
        {
            await _empleadoData.CambiarEstadoEmpleadoAsync(id, estado);
        }
        #endregion

        #region Usuarios Disponibles (para dropdown)
        public async Task<List<SelectListItem>> ObtenerUsuariosDisponiblesAsync()
        {
            var usuarios = await _usuarioData.ListarUsuariosPorFiltroAsync(null, null, null);

            var rolesPermitidos = new[] { "Admin", "Conserje", "Vendedor" };

            var usuariosFiltrados = usuarios
                .Where(u => rolesPermitidos.Contains(u.Rol.Nombre))
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = $"{u.Gmail} - {u.Rol.Nombre}"
                })
                .ToList();

            return usuariosFiltrados;
        }
        #endregion
    }
}
