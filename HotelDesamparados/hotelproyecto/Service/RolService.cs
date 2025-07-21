using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class RolService
    {
        private readonly RolData _rolData;

        public RolService(RolData rolData)
        {
            _rolData = rolData;
        }

        #region "Crear"
        public async Task CrearRolAsync(RolViewModel vm)
        {
            var rol = new Rol
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                Estado = vm.Estado
            };

            await _rolData.CrearRolAsync(rol);
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarRolAsync(RolViewModel vm)
        {
            var rol = new Rol
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion
            };

            await _rolData.ActualizarRolAsync(rol);
        }
        #endregion

        #region "Listar"
        public async Task<List<RolViewModel>> ListarRolesAsync()
        {
            var roles = await _rolData.ListarRolesAsync();
            return roles.Select(rol => new RolViewModel
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Estado = rol.Estado
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<RolViewModel?> ObtenerRolViewModelPorIdAsync(int id)
        {
            var rol = await _rolData.ObtenerRolPorIdAsync(id);
            if (rol == null) return null;

            return new RolViewModel
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Estado = rol.Estado
            };
        }
        #endregion

        #region "Cambiar Estado"
        public async Task CambiarEstadoAsync(int id, bool estado)
        {
            await _rolData.CambiarEstadoRolAsync(id, estado);
        }
        #endregion

        #region "Buscar por Nombre"
        public async Task<int?> ObtenerRolPorNombreAsync(string nombre)
        {
            return await _rolData.ObtenerRolPorNombreAsync(nombre);
        }
        #endregion
    }
}
