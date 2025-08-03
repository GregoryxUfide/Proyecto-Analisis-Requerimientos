using hotelproyecto.Services;
using Microsoft.AspNetCore.Mvc;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class PuntoVentaController : Controller
    {
        private readonly PuntoVentaService _puntoVentaService;

        public PuntoVentaController(PuntoVentaService puntoVentaService)
        {
            _puntoVentaService = puntoVentaService;
        }

        #region "Listar"
        public async Task<IActionResult> Index()
        {
            var puntosVenta = await _puntoVentaService.ListarPuntosVentaViewModelAsync();
            return View(puntosVenta);
        }
        #endregion

        #region "Crear"
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var vm = new PuntoVentaViewModel
            {
                Reservas = await _puntoVentaService.ObtenerReservasSelectListAsync(),
                Empleados = await _puntoVentaService.ObtenerEmpleadosSelectListAsync()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(PuntoVentaViewModel vm)
        {                      
            if (!ModelState.IsValid)
            {
                vm.Reservas = await _puntoVentaService.ObtenerReservasSelectListAsync();
                vm.Empleados = await _puntoVentaService.ObtenerEmpleadosSelectListAsync();
                return View(vm);
            }

            await _puntoVentaService.CrearPuntoVentaAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Editar"
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _puntoVentaService.ObtenerPuntoVentaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            vm.Reservas = await _puntoVentaService.ObtenerReservasSelectListAsync();
            vm.Empleados = await _puntoVentaService.ObtenerEmpleadosSelectListAsync();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(PuntoVentaViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Reservas = await _puntoVentaService.ObtenerReservasSelectListAsync();
                vm.Empleados = await _puntoVentaService.ObtenerEmpleadosSelectListAsync();
                return View(vm);
            }

            await _puntoVentaService.ActualizarPuntoVentaAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region "Detalles"
        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var vm = await _puntoVentaService.ObtenerPuntoVentaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm); // Crea la vista Detalles.cshtml para mostrar la info
        }
        #endregion
    }
}
