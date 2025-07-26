using hotelproyecto.Services;
using hotelproyecto.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace hotelproyecto.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly EmpleadoService _empleadoService;

        public EmpleadoController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        #region Index        
        public async Task<IActionResult> Index()
        {
            var empleados = await _empleadoService.ListarEmpleadosViewModelAsync();
            return View(empleados);
        }
        #endregion

        #region Crear        
        public async Task<IActionResult> Crear()
        {
            var vm = new EmpleadoViewModel
            {
                UsuariosDisponibles = await _empleadoService.ObtenerUsuariosDisponiblesAsync() ?? new List<SelectListItem>()
            };
            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(EmpleadoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.UsuariosDisponibles = await _empleadoService.ObtenerUsuariosDisponiblesAsync();
                return View(vm);
            }

            await _empleadoService.CrearEmpleadoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Editar        
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _empleadoService.ObtenerEmpleadoViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            vm.UsuariosDisponibles = await _empleadoService.ObtenerUsuariosDisponiblesAsync();

            return View(vm);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, EmpleadoViewModel vm)
        {
            if (id != vm.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                vm.UsuariosDisponibles = await _empleadoService.ObtenerUsuariosDisponiblesAsync();
                return View(vm);
            }

            await _empleadoService.ActualizarEmpleadoAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region CambiarEstado        
        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _empleadoService.CambiarEstadoEmpleadoAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region Detalles
        public async Task<IActionResult> Detalles(int id)
        {
            var vm = await _empleadoService.ObtenerEmpleadoViewModelPorIdAsync(id);
            if (vm == null) return NotFound();
            
            vm.UsuariosDisponibles = await _empleadoService.ObtenerUsuariosDisponiblesAsync();

            return View(vm);
        }
        #endregion
    }
}
