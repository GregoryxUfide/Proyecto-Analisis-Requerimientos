using hotelproyecto.Services;
using Microsoft.AspNetCore.Mvc;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class ReservaController : Controller
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {            
            await _reservaService.ActualizarReservasFinalizadasAsync();

            var reservas = await _reservaService.ListarReservasViewModelAsync();
            return View(reservas);
        }
        #endregion

        #region "Crear"
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var vm = new ReservaViewModel
            {
                Habitaciones = await _reservaService.ObtenerHabitacionesSelectListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ReservaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Habitaciones = await _reservaService.ObtenerHabitacionesSelectListAsync();
                return View(vm);
            }

            await _reservaService.CrearReservaAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Editar"
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _reservaService.ObtenerReservaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            vm.Habitaciones = await _reservaService.ObtenerHabitacionesSelectListAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ReservaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Habitaciones = await _reservaService.ObtenerHabitacionesSelectListAsync();
                return View(vm);
            }

            await _reservaService.ActualizarReservaAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Detalles"
        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var vm = await _reservaService.ObtenerReservaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        #endregion
    }
}
