using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class HabitacionController : Controller
    {
        private readonly HabitacionService _habitacionService;

        public HabitacionController(HabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {
            var lista = await _habitacionService.ListarHabitacionesAsync();
            return View(lista);
        }
        #endregion

        #region "Crear"
        public IActionResult Crear()
        {
            return View(new HabitacionViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(HabitacionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            try
            {
                await _habitacionService.CrearHabitacionAsync(vm);
                TempData["Success"] = "Habitación creada exitosamente!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear la habitación: " + ex.Message);
                return View(vm);
            }
        }
        #endregion

        #region "Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _habitacionService.ObtenerHabitacionViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(HabitacionViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _habitacionService.ActualizarHabitacionAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Detalles"
        public async Task<IActionResult> Detalles(int id)
        {
            var habitacion = await _habitacionService.ObtenerHabitacionViewModelPorIdAsync(id);
            if (habitacion == null) return NotFound();

            return View(habitacion);
        }
        #endregion

        #region "Cambiar Estado"
        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, bool estado)
        {
            await _habitacionService.CambiarEstadoHabitacionAsync(id, estado);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
