using Microsoft.AspNetCore.Mvc;
using Sprint_2.Services;
using Sprint_2.ViewModel;
using System;
using System.Threading.Tasks;

namespace Sprint_2.Controllers
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
            var lista = await _reservaService.ListarReservasAsync();
            return View(lista);
        }
        #endregion

        #region "ListarPorFecha"
        [HttpPost]
        public async Task<IActionResult> ListarPorFecha(DateTime fechaDesde)
        {
            if (fechaDesde == default)
            {
                ModelState.AddModelError("", "Debe seleccionar una fecha válida.");
                var listaVacia = await _reservaService.ListarReservasAsync();
                return View("Index", listaVacia);
            }

            var lista = await _reservaService.ListarReservasPorFechaInicioAsync(fechaDesde);
            return View("Index", lista);
        }
        #endregion

        #region "Crear"
        public IActionResult Crear()
        {
            return View(new ReservaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ReservaViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _reservaService.CrearReservaAsync(vm);
                TempData["Success"] = "Reserva creada exitosamente!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al crear la reserva: {ex.Message}");
                return View(vm);
            }
        }
        #endregion

        #region "Editar"
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _reservaService.ObtenerReservaViewModelPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ReservaViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            try
            {
                await _reservaService.ActualizarReservaAsync(vm);
                TempData["Success"] = "Reserva actualizada exitosamente!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al actualizar la reserva: {ex.Message}");
                return View(vm);
            }
        }
        #endregion

        #region "Eliminar"
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                await _reservaService.EliminarReservaAsync(id);
                TempData["Success"] = "Reserva eliminada exitosamente!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error al eliminar la reserva: {ex.Message}";
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
