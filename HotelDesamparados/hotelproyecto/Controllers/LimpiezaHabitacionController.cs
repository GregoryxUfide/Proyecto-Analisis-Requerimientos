using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Services;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class LimpiezaHabitacionController : Controller
    {
        private readonly LimpiezaHabitacionService _limpiezaService;
        private readonly HabitacionService _habitacionService;

        public LimpiezaHabitacionController(LimpiezaHabitacionService limpiezaService, HabitacionService habitacionService)
        {
            _limpiezaService = limpiezaService;
            _habitacionService = habitacionService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {
            var lista = await _limpiezaService.ListarLimpiezasAsync();
            return View(lista);
        }
        #endregion

        #region "Crear"
        public async Task<IActionResult> Crear()
        {
            var vm = new LimpiezaHabitacionViewModel
            {
                Habitaciones = await _habitacionService.ListarHabitacionesSuciasAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(LimpiezaHabitacionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Habitaciones = await _habitacionService.ListarHabitacionesSuciasAsync();
                return View(vm);
            }

            if (vm.FotoArchivo != null && vm.FotoArchivo.Length > 0)
            {
                using var memoryStream = new MemoryStream();
                await vm.FotoArchivo.CopyToAsync(memoryStream);
                vm.Foto = memoryStream.ToArray();
            }

            await _limpiezaService.CrearLimpiezaAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _limpiezaService.ObtenerLimpiezaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            // Si no se debe cambiar habitación, no cargues la lista
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(LimpiezaHabitacionViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm); // no cargamos habitaciones, no editables
            }

            await _limpiezaService.ActualizarLimpiezaAsync(vm);
            return RedirectToAction("Index");
        }
        #endregion

        #region "Detalles"
        public async Task<IActionResult> Detalles(int id)
        {
            var item = await _limpiezaService.ObtenerLimpiezaViewModelPorIdAsync(id);
            if (item == null)
                return NotFound();

            return View(item);
        }
        #endregion

        #region "Buscar por Conserje"
        public async Task<IActionResult> BuscarPorConserje(string nombreConserje)
        {
            if (string.IsNullOrWhiteSpace(nombreConserje))
                return View("Index", new List<LimpiezaHabitacionViewModel>());

            var resultados = await _limpiezaService.ListarLimpiezasPorConserjeAsync(nombreConserje);
            return View("Index", resultados);
        }
        #endregion
    }
}
