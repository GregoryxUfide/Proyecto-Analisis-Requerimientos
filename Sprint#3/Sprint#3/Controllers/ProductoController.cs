using Microsoft.AspNetCore.Mvc;
using Sprint2.ViewModel;
using Sprint_2.Services;

namespace Sprint_2.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService _productoService;

        public ProductoController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {
            var productos = await _productoService.ListarProductosViewModelAsync();
            return View(productos);
        }
        #endregion

        #region "Crear"
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var vm = new ProductoViewModel
            {
                Ubicaciones = await _productoService.ObtenerUbicacionesSelectListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Ubicaciones = await _productoService.ObtenerUbicacionesSelectListAsync();
                return View(vm);
            }

            await _productoService.CrearProductoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Editar"
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _productoService.ObtenerProductoViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            // Ya trae las ubicaciones cargadas, si no, se podría cargar aquí también
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Ubicaciones = await _productoService.ObtenerUbicacionesSelectListAsync();
                return View(vm);
            }

            await _productoService.EditarProductoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Estado"
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _productoService.CambiarEstadoProductoAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
