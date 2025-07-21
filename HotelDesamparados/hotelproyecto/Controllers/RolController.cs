using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{    
    public class RolController : Controller
    {
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {
            var lista = await _rolService.ListarRolesAsync();
            return View(lista);
        }
        #endregion

        #region "Crear"
        public IActionResult Crear()
        {
            return View(new RolViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(RolViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _rolService.CrearRolAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _rolService.ObtenerRolViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(RolViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _rolService.ActualizarRolAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Detalles"
        public async Task<IActionResult> Detalles(int id)
        {
            var rol = await _rolService.ObtenerRolViewModelPorIdAsync(id);

            if (rol == null)
                return NotFound();

            return View(rol);
        }
        #endregion

        #region "Estado"
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _rolService.CambiarEstadoAsync(id, estado);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
