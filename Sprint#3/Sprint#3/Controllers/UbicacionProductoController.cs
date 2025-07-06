using Microsoft.AspNetCore.Mvc;
using Sprint_2.Services;
using Sprint_2.ViewModel;

namespace Sprint_2.Controllers
{
    public class UbicacionProductoController : Controller
    {
        private readonly UbicacionProductoService _ubicacionService;

        public UbicacionProductoController(UbicacionProductoService ubicacionService)
        {
            _ubicacionService = ubicacionService;
        }

        #region "Listar"       
        public async Task<IActionResult> Index()
        {
            var lista = await _ubicacionService.ListarUbicacionesAsync();
            return View(lista);
        }
        #endregion

        #region "Crear"       
        public IActionResult Crear()
        {
            return View(new UbicacionProductoViewModel());
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(UbicacionProductoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _ubicacionService.CrearUbicacionAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Editar"        
        public async Task<IActionResult> Editar(int id)
        {
            var ubicacion = await _ubicacionService.ObtenerUbicacionPorIdAsync(id);
            if (ubicacion == null)
                return NotFound();

            return View(ubicacion);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(UbicacionProductoViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _ubicacionService.ActualizarUbicacionAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Detalles"
        public async Task<IActionResult> Detalles(int id)
        {
            var ubicacion = await _ubicacionService.ObtenerUbicacionPorIdAsync(id);
            if (ubicacion == null)
                return NotFound();

            return View(ubicacion);
        }
        #endregion

        #region "Cambiar Estado"        
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _ubicacionService.CambiarEstadoUbicacionAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
