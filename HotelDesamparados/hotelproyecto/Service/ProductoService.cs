using hotelproyecto.Data;
using hotelproyecto.Models;
using hotelproyecto.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.Services
{
    public class ProductoService
    {
        private readonly ProductoData _productoData;
        private readonly UbicacionProductoService _ubicacionProductoService;

        public ProductoService(ProductoData productoData, UbicacionProductoService ubicacionProductoService)
        {
            _productoData = productoData;
            _ubicacionProductoService = ubicacionProductoService;
        }

        #region "Listar"
        public async Task<List<ProductoViewModel>> ListarProductosViewModelAsync()
        {
            var productos = await _productoData.ListarProductosAsync();

            return productos.Select(p => new ProductoViewModel
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                DescripcionProducto = p.DescripcionProducto,
                NombreUbicacion = p.UbicacionProducto?.NombreUbicacionProducto,
                CantidadProducto = p.CantidadProducto,
                CaducidadProducto = p.CaducidadProducto,
                MarcaProducto = p.MarcaProducto,
                EstadoProducto = p.EstadoProducto
            }).ToList();
        }
        #endregion

        #region "Obtener por Id"
        public async Task<ProductoViewModel?> ObtenerProductoViewModelPorIdAsync(int id)
        {
            var producto = await _productoData.ObtenerProductoPorIdAsync(id);
            if (producto == null) return null;

            var ubicaciones = await _ubicacionProductoService.ListarUbicacionesAsync();

            return new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                DescripcionProducto = producto.DescripcionProducto,
                IdUbicacionProducto = producto.IdUbicacionProducto,
                CantidadProducto = producto.CantidadProducto,
                CaducidadProducto = producto.CaducidadProducto,
                MarcaProducto = producto.MarcaProducto,
                EstadoProducto = producto.EstadoProducto,
                Ubicaciones = ubicaciones.Select(u => new SelectListItem
                {
                    Value = u.IdUbicacionProducto.ToString(),
                    Text = u.NombreUbicacionProducto
                }).ToList()
            };
        }
        #endregion

        #region "Crear"
        public async Task CrearProductoAsync(ProductoViewModel vm)
        {
            var producto = new Producto
            {
                NombreProducto = vm.NombreProducto,
                DescripcionProducto = vm.DescripcionProducto,
                IdUbicacionProducto = vm.IdUbicacionProducto,
                CantidadProducto = vm.CantidadProducto,
                CaducidadProducto = vm.CaducidadProducto,
                MarcaProducto = vm.MarcaProducto,
                EstadoProducto = vm.EstadoProducto
            };

            await _productoData.CrearProductoAsync(producto);
        }
        #endregion

        #region "Actualizar"
        public async Task EditarProductoAsync(ProductoViewModel vm)
        {
            var producto = new Producto
            {
                IdProducto = vm.IdProducto,
                NombreProducto = vm.NombreProducto,
                DescripcionProducto = vm.DescripcionProducto,
                IdUbicacionProducto = vm.IdUbicacionProducto,
                CantidadProducto = vm.CantidadProducto,
                CaducidadProducto = vm.CaducidadProducto,
                MarcaProducto = vm.MarcaProducto,
                EstadoProducto = vm.EstadoProducto
            };

            await _productoData.EditarProductoAsync(producto);
        }
        #endregion

        #region "Estado"
        public async Task CambiarEstadoProductoAsync(int id, bool estado)
        {
            await _productoData.EstadoProductoAsync(id, estado);
        }
        #endregion

        #region "Cargar Ubicaciones"
        public async Task<List<SelectListItem>> ObtenerUbicacionesSelectListAsync()
        {
            var ubicaciones = await _ubicacionProductoService.ListarUbicacionesActivasAsync();

            return ubicaciones.Select(u => new SelectListItem
            {
                Value = u.IdUbicacionProducto.ToString(),
                Text = u.NombreUbicacionProducto
            }).ToList();
        }
        #endregion
    }
}
