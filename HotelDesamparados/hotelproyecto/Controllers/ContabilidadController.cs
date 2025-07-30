using Microsoft.AspNetCore.Mvc;
using hotelproyecto.Service;
using hotelproyecto.ViewModel;

namespace hotelproyecto.Controllers
{
    public class ContabilidadController : Controller
    {
        private readonly ContabilidadService _contabilidadService;

        public ContabilidadController(ContabilidadService contabilidadService)
        {
            _contabilidadService = contabilidadService;
        }


        #region "Listar"
        public async Task<IActionResult> Index(int? mes, int? anio)
        {
            var lista = await _contabilidadService.FiltrarPorFechaAsync(mes, anio);

            ViewBag.MesActual = mes;
            ViewBag.AnioActual = anio;

            return View(lista);
        }



        //public async Task<IActionResult> Index()
        //{
        //    var lista = await _contabilidadService.ListarContabilidadAsync();
        //    return View(lista);
        //}
        #endregion


        #region "Crear"
        [HttpGet]
        public IActionResult Crear()
        {
            return View(new ContabilidadViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ContabilidadViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _contabilidadService.CrearContabilidadAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region "Editar"
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var vm = await _contabilidadService.ObtenerContabilidadPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ContabilidadViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _contabilidadService.ActualizarContabilidadAsync(vm);
            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region "Detalles"
        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var vm = await _contabilidadService.ObtenerContabilidadPorIdAsync(id);
            if (vm == null) return NotFound();

            return View(vm);
        }
        #endregion

    }
}
