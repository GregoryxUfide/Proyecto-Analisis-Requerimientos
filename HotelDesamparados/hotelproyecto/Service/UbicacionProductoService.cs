using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Services
{
    public class UbicacionProductoService
    {
        private readonly UbicacionProductoData _ubicacionData;

        public UbicacionProductoService(UbicacionProductoData ubicacionData)
        {
            _ubicacionData = ubicacionData;
        }

        #region "Listar"
        public async Task<List<UbicacionProductoViewModel>> ListarUbicacionesAsync()
        {
            var lista = await _ubicacionData.ListarUbicacionesAsync();
            return lista.Select(u => new UbicacionProductoViewModel
            {
                IdUbicacionProducto = u.IdUbicacionProducto,
                NombreUbicacionProducto = u.NombreUbicacionProducto,
                DescripcionUbicacionProducto = u.DescripcionUbicacionProducto,
                Estado = u.Estado
            }).ToList();
        }
        #endregion

        #region "Listar Activos"
        public async Task<List<UbicacionProductoViewModel>> ListarUbicacionesActivasAsync()
        {
            var lista = await _ubicacionData.ListarUbicacionesAsync();
            var activas = lista.Where(u => u.Estado).ToList();  // Solo activas

            return activas.Select(u => new UbicacionProductoViewModel
            {
                IdUbicacionProducto = u.IdUbicacionProducto,
                NombreUbicacionProducto = u.NombreUbicacionProducto,
                DescripcionUbicacionProducto = u.DescripcionUbicacionProducto,
                Estado = u.Estado
            }).ToList();
        }
        #endregion

        #region "Crear"
        public async Task CrearUbicacionAsync(UbicacionProductoViewModel vm)
        {
            var ubicacion = new UbicacionProducto
            {
                NombreUbicacionProducto = vm.NombreUbicacionProducto,
                DescripcionUbicacionProducto = vm.DescripcionUbicacionProducto,
                Estado = vm.Estado
            };

            await _ubicacionData.CrearUbicacionAsync(ubicacion);
        }
        #endregion

        #region "Obtener por ID"
        public async Task<UbicacionProductoViewModel?> ObtenerUbicacionPorIdAsync(int id)
        {
            var ubicacion = await _ubicacionData.ObtenerUbicacionPorIdAsync(id);
            if (ubicacion == null) return null;

            return new UbicacionProductoViewModel
            {
                IdUbicacionProducto = ubicacion.IdUbicacionProducto,
                NombreUbicacionProducto = ubicacion.NombreUbicacionProducto,
                DescripcionUbicacionProducto = ubicacion.DescripcionUbicacionProducto,
                Estado = ubicacion.Estado
            };
        }
        #endregion

        #region "Actualizar"
        public async Task ActualizarUbicacionAsync(UbicacionProductoViewModel vm)
        {
            var ubicacion = new UbicacionProducto
            {
                IdUbicacionProducto = vm.IdUbicacionProducto,
                NombreUbicacionProducto = vm.NombreUbicacionProducto,
                DescripcionUbicacionProducto = vm.DescripcionUbicacionProducto,
                Estado = vm.Estado
            };

            await _ubicacionData.ActualizarUbicacionAsync(ubicacion);
        }
        #endregion

        #region "Cambiar Estado"
        public async Task CambiarEstadoUbicacionAsync(int id, bool estado)
        {
            await _ubicacionData.CambiarEstadoUbicacionAsync(id, estado);
        }
        #endregion
    }
}
